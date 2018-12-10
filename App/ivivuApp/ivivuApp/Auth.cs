using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivivuApp
{
    class Employee
    {
        public int maNV;
        public String hoTen;
        public String tenDangNhap;
        public String matKhau;
        public int maKS;
        public Employee()
        {
            maNV = 0;
            hoTen = "";
            tenDangNhap = "";
            matKhau = "";
            maKS = 0;
        }
    }

    class User
    {
        public int maKH;
        public String hoTen;
        public String tenDangNhap;
        public String matKhau;
        public String soCMND;
        public String diaChi;
        public String soDienThoai;
        public String moTa;
        public String email;
        public User()
        {
            maKH = 0;
            hoTen = "";
            tenDangNhap = "";
            matKhau = "";
            soCMND = "";
            diaChi = "";
            soDienThoai = "";
            moTa = "";
            email = "";
        }
    }

    class Auth
    {
        public static bool isCustomerLogged = false;
        public static bool isEmployeeLogged = false;
        public static User user = new User();
        public static Employee employee = new Employee();
    }
}
