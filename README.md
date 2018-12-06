# ivivu
Đồ án môn CSDL NC CQ2016/1
# Cấu trúc repo
```bash
Root
|-DB
| |-models (Lưu model của CSDL)
| |-scripts (Lưu các script liên quan đến CSDL)
|-App (Lưu ứng dụng)
|-Report (Lưu file báo cáo)
```

App.config
```bash
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
    </startup>
<connectionStrings>
  <add name="DataSource" connectionString="ADMIN\SQLEXPRESS" providerName="System.Data.SqlClient" />
  <add name="UserID" connectionString="ivivu" providerName="System.Data.SqlClient" />
  <add name="Password" connectionString="123456" providerName="System.Data.SqlClient" />
  <add name="InitialCatalog" connectionString="QLKhachSan" providerName="System.Data.SqlClient" />
    </connectionStrings>
</configuration>
```