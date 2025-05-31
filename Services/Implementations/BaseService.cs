using AutoMapper;
using LemonHiveEcommerce.Repositories.Interfaces;
using LemonHiveEcommerce.Services.Interfaces;

namespace LemonHiveEcommerce.Services.Implementations
{
    public class BaseService<TDto, TEntity> : IBaseService<TDto>
        where TDto : class
        where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected virtual IGenericRepository<TEntity> Repository
        {
            get
            {
                throw new NotImplementedException("Override this property in child service to provide repository");
            }
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await Repository.GetAll();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await Repository.GetById(id);
            return entity == null ? null : _mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> AddAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                await Repository.Add(entity);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<bool> UpdateAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                await Repository.Update(entity);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                await Repository.Delete(id);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
