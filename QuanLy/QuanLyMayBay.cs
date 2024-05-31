using System;
using System.IO;
using System.Text;
using ThuVien;

namespace QuanLyMayBay
{
    public class QuanLyMayBay
    {
        public string ChuoiChuanHoa(string str)
        {
            int i = 0;
            while (i < str.Length && str[i] == ' ')
            {
                i++;
            }
            int j = str.Length - 1;
            while (j >= 0 && str[j] == ' ')
            {
                j--;
            }
            if (i > j)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            while (i <= j)
            {
                if (str[i] != ' ')
                {
                    sb.Append(str[i]);
                }
                else if (sb[sb.Length - 1] != ' ')
                {
                    sb.Append(str[i]);
                }
                i++;
            }
            return sb.ToString();
        }

        public string ChuoiVietHoa(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'a' && str[i] <= 'z')
                {
                    sb.Append((char)(str[i] - 32));
                }
                else
                {
                    sb.Append(str[i]);
                }
            }
            return sb.ToString();
        }

        public int nhapSo()
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
                    Console.Write("Nhập lại: ");
                }
            }
            while (!isInt);
            return so;
        }

        public bool KTraChuoiTrung(string a, string b)
        {
            int i = 0;
            bool kq = true;
            if (a.Length == b.Length)
            {
                while (i < a.Length && i < b.Length)
                {

                    if (a[i] != b[i])
                    {
                        kq = false;
                        break;
                    }
                    i++;                                     
                }
            }
            else
            {
                kq = false;
            }
            return kq;
        }

        public bool KTraTrung(ref DSMayBay dsmb, string a)
        {
            bool kq = false;
            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                if (KTraChuoiTrung(a, dsmb.data[i].soHieuMayBay))
                {
                    kq = true;
                    break;
                }
            }
            return kq;
        }

        public void SoLuongBay(ref DSMayBay dsmb, NodeCB nodeCB)
        {
            DateTime now = DateTime.Now;
            int nam = now.Year;
            int thang = now.Month;
            int ngay = now.Day;
            int gio = now.Hour;
            int phut = now.Minute;
            QuanLyChuyenBay.QuanLyChuyenBay qlcb = new QuanLyChuyenBay.QuanLyChuyenBay();
            qlcb.LoadFileCB(ref nodeCB);
            NodeCB get;
            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                dsmb.data[i].sl_Bay = 0;
                for (get = nodeCB; get != null; get = get.next)
                {
                    if (KTraChuoiTrung(dsmb.data[i].soHieuMayBay, get.data.soHieuMB))
                    {
                        if (get.data.thoiGianXP.nam < nam)
                        {
                            dsmb.data[i].sl_Bay += 1;
                        }
                        else if (get.data.thoiGianXP.nam == nam)
                        {
                            if (get.data.thoiGianXP.thang < thang)
                            {
                                dsmb.data[i].sl_Bay += 1;
                            }
                            else if (get.data.thoiGianXP.thang == thang)
                            {
                                if (get.data.thoiGianXP.ngay < ngay)
                                {
                                    dsmb.data[i].sl_Bay += 1;
                                }
                                else if (get.data.thoiGianXP.ngay == ngay)
                                {
                                    if (get.data.thoiGianXP.gio < gio)
                                    {
                                        dsmb.data[i].sl_Bay += 1;
                                    }
                                    else if (get.data.thoiGianXP.gio == gio)
                                    {
                                        if (get.data.thoiGianXP.phut < phut)
                                        {
                                            dsmb.data[i].sl_Bay += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ThongKeMB(ref DSMayBay dsmb)
        {
            StreamWriter f = new StreamWriter("D:\\Ctdl&gt\\LabCuoiKy\\ThongKeLuotBay.txt");

            if (f == null)
            {
                f.WriteLine("0");
            }
            for (int i = 0; i < dsmb.so_MayBay - 1; i++)
            {
                for (int j = i + 1; j < dsmb.so_MayBay; j++)
                {
                    if (dsmb.data[i].sl_Bay < dsmb.data[j].sl_Bay)
                    {
                        MayBay temp = dsmb.data[i];
                        dsmb.data[i] = dsmb.data[j];
                        dsmb.data[j] = temp;
                    }
                }
            }

            Console.Write("Số hiệu máy bay");
            Console.WriteLine("\t\tSố lượt thực hiện chuyến bay");
            f.Write("Số hiệu máy bay");
            f.WriteLine("\t\tSố lượt thực hiện chuyến bay");

            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                Console.Write("\t" + dsmb.data[i].soHieuMayBay);
                Console.WriteLine("\t\t\t" + dsmb.data[i].sl_Bay);
                f.Write("\t" + dsmb.data[i].soHieuMayBay);
                f.WriteLine("\t\t\t\t\t" + dsmb.data[i].sl_Bay);
            }
            f.Close();
        }

        public void ThemMB(ref DSMayBay dsmb)
        {
            dsmb.data[dsmb.so_MayBay] = new MayBay();
            string soHieuMB;
            do
            {
                Console.Write("Nhập số hiệu máy bay: ");
                soHieuMB = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if (KTraTrung(ref dsmb, soHieuMB))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số hiệu máy bay này đã tồn tại!");
                    Console.ResetColor();
                }
                if(soHieuMB.Length > 15 || soHieuMB == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;                   
                    Console.WriteLine("Số kí tự của số hiệu máy bay nhập không quá 15 kí tự và không được để trống!");
                    Console.ResetColor();
                }
            }
            while (KTraTrung(ref dsmb, soHieuMB) || soHieuMB.Length > 15 || soHieuMB == "");
            dsmb.data[dsmb.so_MayBay].soHieuMayBay = soHieuMB;

            do
            {
                Console.Write("Nhập loại máy bay: ");
                dsmb.data[dsmb.so_MayBay].loaiMayBay = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if(dsmb.data[dsmb.so_MayBay].loaiMayBay.Length > 40 || dsmb.data[dsmb.so_MayBay].loaiMayBay == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số kí tự của loại hiệu máy bay nhập không quá 40 kí tự và không được để trống!");
                    Console.ResetColor();
                }
            }
            while (dsmb.data[dsmb.so_MayBay].loaiMayBay.Length > 40 || dsmb.data[dsmb.so_MayBay].loaiMayBay == "");            
            int sc;
            do
            {
                Console.Write("Nhập số lượng chỗ ngồi: ");
                sc = nhapSo();
                if(sc < 20)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số chỗ ngồi của máy bay ít nhất là 20");
                    Console.ResetColor();
                }
            }
            while (sc < 20);
            dsmb.data[dsmb.so_MayBay].soCho = sc;
            dsmb.so_MayBay++;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Thêm máy bay mới mang số hiệu " + soHieuMB + " vào danh sách thành công.");
            Console.ResetColor();
        }

        public void ChinhSuaMB(ref DSMayBay dsmb)
        {
            string soHieuMB;
            do
            {
                Console.Write("Nhập số hiệu máy bay: ");
                soHieuMB = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if(KTraTrung(ref dsmb, soHieuMB) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số hiệu máy bay này không tồn tại. Xin kiểm tra lại!!");
                    Console.ResetColor();
                }
            }
            while (KTraTrung(ref dsmb, soHieuMB) == false);
            Console.WriteLine("---Chỉnh sửa máy bay---");
            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                if (KTraChuoiTrung(dsmb.data[i].soHieuMayBay, soHieuMB))
                {
                    string loaiMBTemp = "";
                    int soChoTemp = 0;
                    do
                    {
                        Console.Write("Chỉnh sửa loại máy bay: ");
                        loaiMBTemp = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                        if (loaiMBTemp.Length > 40 || loaiMBTemp == "")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Số kí tự tối đa của loại máy bay là 40 và không được để trống!");
                            Console.ResetColor();
                        }
                    }
                    while (loaiMBTemp.Length > 40 || loaiMBTemp == "");
                    do
                    {
                        Console.Write("Chỉnh sửa số chỗ: ");
                        soChoTemp = nhapSo();
                        if (soChoTemp < 20)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Số chỗ ngồi của máy bay ít nhất là 20");
                            Console.ResetColor();
                        }
                    }
                    while (soChoTemp < 20);
                    if (loaiMBTemp != "")
                    {
                        dsmb.data[i].loaiMayBay = loaiMBTemp;
                    }
                    if (soChoTemp != 0)
                    {
                        dsmb.data[i].soCho = soChoTemp;
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Chỉnh sửa dữ liệu máy bay mang số hiệu " + dsmb.data[i].soHieuMayBay + " thành công.");
                    Console.ResetColor();
                    break;
                }
                if (i == dsmb.so_MayBay - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Không tìm thấy số hiệu máy bay bạn muốn chỉnh sửa!");
                    Console.ResetColor();
                }
                if (dsmb.so_MayBay == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Danh sách máy bay rỗng!!");
                    Console.ResetColor();
                }
            }
        }

        public void InDSMB(ref DSMayBay dsmb)
        {
            Console.Write("Số hiệu máy bay");
            Console.WriteLine("\t\tLoại máy bay");
            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                Console.WriteLine(dsmb.data[i].soHieuMayBay + "\t\t\t" + dsmb.data[i].loaiMayBay);
            }
        }

        public void XoaMB(ref DSMayBay dsmb)
        {
            Console.Write("Nhập số hiệu máy bay bạn muốn xóa: ");
            string soHieuMB = Console.ReadLine();
            soHieuMB = ChuoiChuanHoa(ChuoiVietHoa(soHieuMB));
            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                if (KTraChuoiTrung(dsmb.data[i].soHieuMayBay, soHieuMB))
                {
                    while (i < dsmb.so_MayBay)
                    {
                        if(i == dsmb.so_MayBay - 1)
                        {
                            break;
                        }
                        dsmb.data[i] = dsmb.data[i + 1];
                        i++;
                    }
                    dsmb.so_MayBay--;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Xóa dữ liệu máy bay mang số hiệu " + dsmb.data[i].soHieuMayBay + " thành công.");
                    Console.ResetColor();
                    break;
                }
                if(i == dsmb.so_MayBay - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Không tìm thấy số hiệu máy bay bạn muốn xoá!");
                    Console.ResetColor();
                }
                if(dsmb.so_MayBay == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Danh sách máy bay rỗng!!");
                    Console.ResetColor();
                }
            }            
        }

        public int LoadFileMB(ref DSMayBay ds)
        {
            StreamReader f = new StreamReader("D:\\Ctdl&gt\\LabCuoiKy\\dsmb.txt");
            if (f == null) return 0;
            ds.so_MayBay = Convert.ToInt32(f.ReadLine());
            f.ReadLine();
            f.ReadLine();
            f.ReadLine();
            f.ReadLine();
            f.ReadLine();
            f.ReadLine();
            for (int i = 0; i < ds.so_MayBay; i++)
            {
                ds.data[i] = new MayBay();
                ds.data[i].soHieuMayBay = f.ReadLine();
                ds.data[i].loaiMayBay = f.ReadLine();
                ds.data[i].soCho = Convert.ToInt32(f.ReadLine());
                ds.data[i].sl_Bay = Convert.ToInt32(f.ReadLine());
                f.ReadLine();
            }
            
            f.Close();
            return 1;
        }

        public int SaveFileMB(ref DSMayBay ds)
        {
            StreamWriter f = new StreamWriter("D:\\Ctdl&gt\\LabCuoiKy\\dsmb.txt");
            if (f == null) return 0;
            f.WriteLine(ds.so_MayBay);
            f.WriteLine("---------------------------------");
            f.WriteLine("Số hiệu máy bay");
            f.WriteLine("Loại máy bay");
            f.WriteLine("Số chỗ ngồi");
            f.WriteLine("Số lần đã thực hiện chuyến bay");
            f.WriteLine("---------------------------------");
            bool trangthai;
            for (int i = 0; i < ds.so_MayBay; i++)
            {
                trangthai = ds.data[i].trangThai;
                f.WriteLine(ds.data[i].soHieuMayBay);
                f.WriteLine(ds.data[i].loaiMayBay);
                f.WriteLine(ds.data[i].soCho.ToString());
                f.WriteLine(ds.data[i].sl_Bay.ToString());
                f.WriteLine("---------------------------------");
            }
            
            f.Close();
            return 1;
        }
    }
}
