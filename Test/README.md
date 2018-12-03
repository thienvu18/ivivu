## Bước 1. Cần tạo connection của VS với SQL SERVER
[LINK](https://ongthovuive.wordpress.com/2016/09/06/c-ket-noi-co-so-du-lieu-sql/)
Dùng Data Source vào phía bên dưới

## Bước 2. Cách để run source

Vào thư mục app tạo file App.config với data như sau:

```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
    </startup>
<connectionStrings>
  <add name="Conn" connectionString="Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLKhachSan;Integrated Security=True" providerName="System.Data.SqlClient" />
    </connectionStrings>
</configuration>
```

Thay connectionString bên trong là connection connect với database