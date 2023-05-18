using System;
using System.Collections.Generic;
using System.Globalization;
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

        bool xau_trung(string a, string b)
        {
            int i = 0;
            while (i < a.Length && i < b.Length)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
                i++;
            }
            return true;
        }

        public bool KTraTrung(DSMayBay dsmb, string a)
        {
            bool kq = false;
            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                if (xau_trung(a, dsmb.data[i].soHieuMayBay))
                {
                    kq = true;
                    break;
                }
            }
            return kq;
        }

        public void ThemMB(DSMayBay dsmb)
        {
            dsmb.data[dsmb.so_MayBay] = new MayBay();
            string soHieuMB;
            do
            {
                Console.Write("Nhập số hiệu máy bay: ");
                soHieuMB = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if (KTraTrung(dsmb, soHieuMB))
                {
                    Console.WriteLine("Số hiệu máy bay này đã tồn tại");

                }
            }
            while (KTraTrung(dsmb, soHieuMB));
            dsmb.data[dsmb.so_MayBay].soHieuMayBay = soHieuMB;
            Console.Write("Nhập loại máy bay: ");
            dsmb.data[dsmb.so_MayBay].loaiMayBay = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
            Console.Write("Nhập số lượng chỗ ngồi: ");
            int sc = int.Parse(Console.ReadLine());
            dsmb.data[dsmb.so_MayBay].soCho = sc;
            dsmb.so_MayBay++;
        }

        public void ChinhSuaMB(DSMayBay dsmb)
        {
            Console.Write("Nhập số hiệu máy bay bạn muốn chỉnh sửa: ");
            string soHieuMB = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                if (KTraTrung(dsmb, soHieuMB))
                {
                    string soHieuTemp = "";
                    string loaiMBTemp = "";
                    int soChoTemp = 0;
                    do
                    {
                        Console.Write("Nhập số hiệu máy bay: ");
                        soHieuTemp = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                        if (KTraTrung(dsmb, soHieuTemp))
                        {
                            Console.WriteLine("Số hiệu máy bay này đã tồn tại");

                        }
                    }
                    while (KTraTrung(dsmb, soHieuTemp));
                    Console.Write("Chỉnh sửa loại máy bay: ");
                    loaiMBTemp = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                    Console.Write("Chỉnh sửa số chỗ: ");
                    soChoTemp = int.Parse(Console.ReadLine());
                    if (soHieuTemp != "")
                    {
                        dsmb.data[i].soHieuMayBay = soHieuTemp;
                    }
                    if (loaiMBTemp != "")
                    {
                        dsmb.data[i].loaiMayBay = loaiMBTemp;
                    }
                    if (soChoTemp != 0)
                    {
                        dsmb.data[i].soCho = soChoTemp;
                    }
                    return;
                }
            }
            Console.WriteLine("Không tìm thấy số hiệu máy bay bạn muốn chỉnh sửa!");
        }


        public void XoaMB(ref DSMayBay dsmb)
        {
            Console.Write("Nhập số hiệu máy bay bạn muốn xóa: ");
            string soHieuMB = Console.ReadLine();
            soHieuMB = ChuoiChuanHoa(ChuoiVietHoa(soHieuMB));
            Console.WriteLine(soHieuMB);
            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                if (KTraTrung(dsmb, soHieuMB))
                {
                    while (i < dsmb.so_MayBay)
                    {
                        dsmb.data[i] = dsmb.data[i + 1];
                        i++;
                    }
                    dsmb.so_MayBay--;
                    Console.WriteLine("Xóa tành công");
                    return;
                }
            }
            Console.WriteLine("Không tìm thấy số hiệu máy bay bạn muốn xoá!");
        }

        public int LoadFileMB(ref DSMayBay ds)
        {
            StreamReader f = new StreamReader(".\\dsmb.txt");
            if (f == null) return 0;
            string tt;
            f.ReadLine();
            ds.so_MayBay = Convert.ToInt32(f.ReadLine());
            for (int i = 0; i < ds.so_MayBay; i++)
            {
                ds.data[i] = new MayBay();
                ds.data[i].soHieuMayBay = f.ReadLine();
                ds.data[i].loaiMayBay = f.ReadLine();
                ds.data[i].soCho = Convert.ToInt32(f.ReadLine());
                ds.data[i].sl_Bay = Convert.ToInt32(f.ReadLine());
                tt = f.ReadLine();
                ds.data[i].trangThai = xau_trung(tt, "false") ? false : true;
            }
            f.Close();
            return 1;
        }

        public int SaveFileMB(ref DSMayBay ds)
        {
            StreamWriter f = new StreamWriter(".\\savedsmb.txt");
            if (f == null) return 0;
            f.WriteLine();
            f.WriteLine(ds.so_MayBay);
            bool trangthai;
            for (int i = 0; i < ds.so_MayBay; i++)
            {
                trangthai = ds.data[i].trangThai;
                f.WriteLine(ds.data[i].soHieuMayBay);
                f.WriteLine(ds.data[i].loaiMayBay);
                f.WriteLine(ds.data[i].soCho.ToString());
                f.WriteLine(ds.data[i].sl_Bay.ToString());
                f.WriteLine(trangthai ? "true" : "false");
            }
            f.Close();
            Console.WriteLine("Đã lưu vào file");
            return 1;
        }

    }
}
