using System;
using ThuVien;
using QuanLyMayBay;
using QuanLyChuyenBay;
using QuanLyHanhKhach;
using System.Text;

namespace QuanLySanBay
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.UTF8;

            //ThuVien.DSMayBay ds = new ThuVien.DSMayBay();
            //QuanLyMayBay.QuanLyMayBay qlmb = new QuanLyMayBay.QuanLyMayBay();
            //qlmb.LoadFileMB(ref ds);
            //qlmb.ThemMB(ds, "VN009", "BOING737", 78);

            //qlmb.XoaMB(ref ds);
            //qlmb.SaveFileMB(ref ds);

            //Hiển thị tiếng Việt
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Quản lý sân bay");
            Console.WriteLine("1. Quản lý máy bay");
            Console.WriteLine("2. Quản lý chuyến bay");
            Console.WriteLine("3. Quản lý khách hàng");
            Console.WriteLine("0. Thoát");

            int luaChon;
            do
            {
                Console.Write("Nhập lựa chọn: ");
                luaChon = int.Parse(Console.ReadLine());
                if (luaChon != 0 && luaChon != 1 && luaChon != 2 && luaChon != 3)
                {
                    Console.WriteLine("Nhập lựa chọn có trong Menu");
                }
            }
            while (luaChon != 0 && luaChon != 1 && luaChon != 2 && luaChon != 3);

            switch (luaChon)
            {
                case 1:
                    Console.WriteLine("Chức năng:");
                    Console.WriteLine("1. Danh sách máy bay");
                    Console.WriteLine("2. Thống kê máy bay");
                    Console.WriteLine("3. Thêm máy bay");
                    Console.WriteLine("4. Chỉnh sửa máy bay");
                    Console.WriteLine("5. Xóa máy bay");


                    int luaChonCN;
                    do
                    {
                        Console.Write("Nhập lựa chọn: ");
                        luaChonCN = int.Parse(Console.ReadLine());
                        if (luaChonCN != 1 && luaChonCN != 2 && luaChonCN != 3 && luaChonCN != 4 && luaChonCN != 5)
                        {
                            Console.WriteLine("Nhập lựa chọn có trong Menu chức năng");
                        }
                    }
                    while (luaChonCN != 1 && luaChonCN != 2 && luaChonCN != 3 && luaChonCN != 4 && luaChonCN != 5);

                    ThuVien.DSMayBay ds = new ThuVien.DSMayBay();
                    QuanLyMayBay.QuanLyMayBay qlmb = new QuanLyMayBay.QuanLyMayBay();
                    switch (luaChonCN)
                    {
                        case 1:

                            break;
                        case 2:
                            break;
                        case 3: //Thêm máy bay
                            qlmb.LoadFileMB(ref ds);
                            qlmb.ThemMB(ds);
                            qlmb.SaveFileMB(ref ds);
                            break;
                        case 4:
                            qlmb.LoadFileMB(ref ds);
                            qlmb.ChinhSuaMB(ds);
                            qlmb.SaveFileMB(ref ds);
                            break;
                        case 5:
                            qlmb.LoadFileMB(ref ds);
                            qlmb.XoaMB(ref ds);
                            qlmb.SaveFileMB(ref ds);
                            break;
                    }
                    break;
            }
        }
    }
}
