using System;
using ThuVien;
using QuanLyMayBay;
using QuanLyChuyenBay;
using QuanLyHanhKhach;
using System.Text;

namespace QuanLySanBay
{
    class Program
    {
        public static int nhapSo()
        {
            int so = 0;
            bool isInt = false;
            do
            {
                try
                {
                    so = int.Parse(Console.ReadLine());
                    isInt = true;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nhập kiểu số nguyên cho dữ liệu này!!");
                    Console.ResetColor();
                }
                if (!isInt)
                {
                    Console.Write("Nhập chức năng: ");
                }
            }
            while (!isInt);
            return so;
        }

        static void Main(string[] args)
        {
            //Hiển thị tiếng Việt
            Console.OutputEncoding = Encoding.UTF8;

            int luaChon;

            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Quản lý sân bay");
                Console.ResetColor();
                Console.WriteLine("1. Quản lý máy bay");
                Console.WriteLine("2. Quản lý chuyến bay và hành khách");
                Console.WriteLine("0. Thoát");
                Console.Write("Nhập lựa chọn: ");
                luaChon = nhapSo();
                Console.Clear();
                switch (luaChon)
                {
                    case 1:
                        int luaChonCN;
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Chức năng quản lý máy bay:");
                            Console.ResetColor();
                            Console.WriteLine("1. In danh sách máy bay");
                            Console.WriteLine("2. Thống kê máy bay");
                            Console.WriteLine("3. Thêm máy bay");
                            Console.WriteLine("4. Chỉnh sửa máy bay");
                            Console.WriteLine("5. Xóa máy bay");
                            Console.WriteLine("0. Thoát quản lý máy bay");
                            Console.Write("Nhập lựa chọn: ");
                            luaChonCN = nhapSo();
                            Console.Clear();
                            DSMayBay ds = new DSMayBay();
                            QuanLyMayBay.QuanLyMayBay qlmb = new QuanLyMayBay.QuanLyMayBay();
                            switch (luaChonCN)
                            {
                                case 1:
                                    qlmb.LoadFileMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("1. In danh sách máy bay");
                                    Console.ResetColor();
                                    qlmb.InDSMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    qlmb.SaveFileMB(ref ds);
                                    break;
                                case 2:
                                    NodeCB nodeCBtemp = new NodeCB();
                                    qlmb.LoadFileMB(ref ds);
                                    qlmb.SoLuongBay(ref ds, nodeCBtemp);
                                    qlmb.SaveFileMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("2. Thống kê máy bay");
                                    Console.ResetColor();
                                    qlmb.ThongKeMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    break;
                                case 3: 
                                    qlmb.LoadFileMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("3. Thêm máy bay");
                                    Console.ResetColor();
                                    qlmb.ThemMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    qlmb.SaveFileMB(ref ds);
                                    break;
                                case 4:
                                    qlmb.LoadFileMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("4. Chỉnh sửa máy bay");
                                    Console.ResetColor();
                                    qlmb.ChinhSuaMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    qlmb.SaveFileMB(ref ds);
                                    break;
                                case 5:
                                    qlmb.LoadFileMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("5. Xóa máy bay");
                                    Console.ResetColor();
                                    qlmb.XoaMB(ref ds);
                                    Console.WriteLine("--------------------------------------------------");
                                    qlmb.SaveFileMB(ref ds);
                                    break;
                                case 0:
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Nhập sai chức năng. Vui lòng nhập lại!");
                                    Console.ResetColor();
                                    break;
                            }
                        }
                        while (luaChonCN != 0);
                        break;
                    case 2:
                        int luaChonCNCB;
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Chức năng quản lý chuyến bay và hành khách:");
                            Console.ResetColor();
                            Console.WriteLine("1. In danh sách tất cả chuyến bay");
                            Console.WriteLine("2. Thêm chuyến bay");
                            Console.WriteLine("3. Chỉnh sửa chuyến bay");
                            Console.WriteLine("4. Hủy chuyến bay");
                            Console.WriteLine("5. Đặt vé");
                            Console.WriteLine("6. Danh sách hành khách theo mã chuyến bay");
                            Console.WriteLine("7. Danh sách chuyến bay theo nơi đến");
                            Console.WriteLine("8. In danh sách vé trống");
                            Console.WriteLine("9. In danh sách tất cả khách hàng");
                            Console.WriteLine("0. Thoát quản lý chuyến bay và hành khách");
                            Console.Write("Nhập lựa chọn: ");
                            luaChonCNCB = nhapSo();
                            Console.Clear();
                            NodeCB nodeCB = new NodeCB();
                            QuanLyChuyenBay.QuanLyChuyenBay qlcb = new QuanLyChuyenBay.QuanLyChuyenBay();

                            switch (luaChonCNCB)
                            {
                                case 1:
                                    qlcb.LoadFileCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("1. In danh sách tất cả chuyến bay");
                                    Console.ResetColor();
                                    qlcb.InTatCaDSCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    break;
                                case 2:
                                    qlcb.LoadFileCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("2. Thêm chuyến bay");
                                    Console.ResetColor();
                                    qlcb.ThemCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    qlcb.SaveFileCB(ref nodeCB);
                                    break;
                                case 3:
                                    qlcb.LoadFileCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("3. Chỉnh sửa chuyến bay");                                   
                                    Console.ResetColor();
                                    qlcb.ChinhSuaCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    qlcb.SaveFileCB(ref nodeCB);
                                    break;
                                case 4:
                                    qlcb.LoadFileCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("4. Hủy chuyến bay");                                   
                                    Console.ResetColor();
                                    qlcb.HuyCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    qlcb.SaveFileCB(ref nodeCB);
                                    break;
                                case 5:
                                    qlcb.LoadFileCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("5. Đặt vé");                                   
                                    Console.ResetColor();
                                    qlcb.DatVe(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    qlcb.SaveFileCB(ref nodeCB);
                                    break;
                                case 6:
                                    qlcb.LoadFileCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("6. Danh sách hành khách theo mã chuyến bay");                                   
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.ResetColor();
                                    qlcb.InDSKHTrenChuyenBay(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    break;
                                case 7:
                                    qlcb.LoadFileCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("7. Danh sách chuyến bay theo nơi đến");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.ResetColor();
                                    qlcb.InDSCBTheoNoiDenTrongNgay(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    break;
                                case 8:
                                    qlcb.LoadFileCB(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("8. In danh sách vé trống");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.ResetColor();
                                    qlcb.InDSVeTrong(ref nodeCB);
                                    Console.WriteLine("--------------------------------------------------");
                                    break;
                                case 9:
                                    QuanLyHanhKhach.QuanLyHanhKhach qlkh = new QuanLyHanhKhach.QuanLyHanhKhach();
                                    NodeKH nodeKH = new NodeKH();
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("9. Danh sách tất cả hành khách");
                                    Console.ResetColor();
                                    qlkh.InTatCaDSKH(ref nodeKH);
                                    Console.WriteLine("--------------------------------------------------");
                                    break;
                                case 0:
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Nhập sai chức năng. Vui lòng nhập lại!");
                                    Console.ResetColor();
                                    break;
                            }
                        }
                        while (luaChonCNCB != 0);
                        break;
                    case 0:
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nhập sai chức năng. Vui lòng nhập lại!");
                        Console.ResetColor();
                        break;
                }
            }
            while (luaChon != 0);  
        }
    }
}
