### 🔄 How to Restore the Database
Ensure your appsettings.json is configured for SQL Server and the connection string is correct.
1. Open SQL Server Management Studio.
2. Right-click **Databases** > **Restore Database**.
3. Choose:
   - **Source**: Device > Add > Select `LemonHiveEcommerce.bak`
   - Set **Destination** as `LemonHiveEcommerce`
4. Click **OK** to restore the database.


### 🔄 If you want to create new
Ensure your appsettings.json is configured for SQL Server and the connection string is correct.
Open PM then..
1. Remove-Migration -Force
2. Drop-Database
3. Add-Migration InitialCreate
4. Update-Database
