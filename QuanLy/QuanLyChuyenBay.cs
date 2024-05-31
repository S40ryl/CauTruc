using QuanLyHanhKhach;
using QuanLyMayBay;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using ThuVien;

namespace QuanLyChuyenBay
{
    public class QuanLyChuyenBay
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

        public bool KTraSoTrongChuoi(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] < 'a' || str[i] > 'z') && (str[i] < 'A' || str[i] > 'Z') && str[i] != ' ')
                {
                    return false;
                }
            }
            return true;
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

        public int ktslcb(NodeCB Frist)
        {
            NodeCB p;
            int i;
            for (p = Frist, i = 0; p != null; p = p.next, i++) ;
            return i;
        }

        public bool checkDateTime(int nam, int thang, int ngay, int gio, int phut)
        {
            bool kq = true;
            DateTime now = DateTime.Now;
            if (nam < now.Year)
            {
                kq = false;
            }
            else if (nam == now.Year)
            {
                if (thang < now.Month)
                {
                    kq = false;
                }
                else if (thang == now.Month)
                {
                    if (ngay < now.Day)
                    {
                        kq = false;
                    }
                    else if (ngay == now.Day)
                    {
                        if (gio < now.Hour)
                        {
                            kq = false;
                        }
                        else if (gio == now.Hour)
                        {
                            if (phut <= now.Minute)
                            {
                                kq = false;
                            }
                        }
                    }
                }
            }
            return kq;
        }

        public void ThemCB(ref NodeCB nodeCB)
        {
            QuanLyMayBay.QuanLyMayBay qlmb = new QuanLyMayBay.QuanLyMayBay();
            DSMayBay dsmb = new DSMayBay();
            qlmb.LoadFileMB(ref dsmb);
            int slcb = ktslcb(nodeCB);
            NodeCB newNode = new NodeCB();
            newNode.sl_Dat = 0;
            newNode.sl_Ve = 0;
            do
            {
                Console.Write("Nhập mã chuyến bay: ");
                newNode.data.maChuyenBay = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if (newNode.data.maChuyenBay.Length > 15 || newNode.data.maChuyenBay == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số kí tự tối đa của mã chuyến bay là 15 kí tự và không được để trống!");
                    Console.ResetColor();
                }
            }
            while (newNode.data.maChuyenBay.Length > 15 || newNode.data.maChuyenBay == "");
            do
            {
                Console.Write("Nhập nơi đến: ");
                newNode.data.noiDen = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if (newNode.data.noiDen == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nơi đến không được để trống!");
                    Console.ResetColor();
                }
            }
            while (newNode.data.noiDen == "");
            do
            {
                Console.Write("Nhập số hiệu máy bay: ");
                newNode.data.soHieuMB = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));

                for (int i = 0; i < dsmb.so_MayBay; i++)
                {
                    if (KTraChuoiTrung(dsmb.data[i].soHieuMayBay, newNode.data.soHieuMB))
                    {
                        newNode.sl_Ve = dsmb.data[i].soCho;
                    }
                }
                if (KTraTrung(ref dsmb, newNode.data.soHieuMB) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số hiệu máy bay này không tồn tại!!");
                    Console.ResetColor();
                }
                if (newNode.data.soHieuMB.Length > 15 || newNode.data.soHieuMB == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số kí tự tối đa của số hiệu máy bay là 15 kí tự và không được để trống!");
                    Console.ResetColor();
                }
            }
            while (KTraTrung(ref dsmb, newNode.data.soHieuMB) == false || newNode.data.soHieuMB.Length > 15 || newNode.data.soHieuMB == "");
            newNode.data.trangThai = 1;
            bool isInt = false;
            Console.WriteLine("---Thời gian khởi hành---");
            do
            {
                do
                {
                    Console.Write("Nhập giờ: ");
                    try
                    {
                        newNode.data.thoiGianXP.gio = int.Parse(Console.ReadLine());
                        isInt = true;
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Giờ có kiểu dữ liệu là kiểu số nguyên");
                        Console.ResetColor();
                    }
                    if (newNode.data.thoiGianXP.gio > 23 || newNode.data.thoiGianXP.gio < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Giờ nhập từ 0 -> 23!!");
                        Console.ResetColor();
                    }
                }
                while (newNode.data.thoiGianXP.gio > 23 || newNode.data.thoiGianXP.gio < 0 || !isInt);

                do
                {
                    try
                    {
                        Console.Write("Nhập phút: ");
                        newNode.data.thoiGianXP.phut = int.Parse(Console.ReadLine());
                        isInt = true;
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Phút có kiểu dữ liệu là kiểu số nguyên");
                        Console.ResetColor();
                    }

                    if (newNode.data.thoiGianXP.phut > 59 || newNode.data.thoiGianXP.phut < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Phút nhập từ 0 -> 59!!");
                        Console.ResetColor();
                    }
                }
                while (newNode.data.thoiGianXP.phut > 59 || newNode.data.thoiGianXP.phut < 0 || !isInt);
                int temp;
                do
                {
                    do
                    {
                        try
                        {
                            Console.Write("Nhập ngày: ");
                            newNode.data.thoiGianXP.ngay = int.Parse(Console.ReadLine());
                            isInt = true;
                        }
                        catch (FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ngày có kiểu dữ liệu là kiểu số nguyên");
                            Console.ResetColor();
                        }
                    }
                    while (!isInt);
                    do
                    {
                        try
                        {
                            Console.Write("Nhập tháng: ");
                            newNode.data.thoiGianXP.thang = int.Parse(Console.ReadLine());
                            isInt = true;
                        }
                        catch (FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Tháng có kiểu dữ liệu là kiểu số nguyên");
                            Console.ResetColor();
                        }

                        if (newNode.data.thoiGianXP.thang > 12 || newNode.data.thoiGianXP.thang < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Trong năm có 12 tháng. Xin kiểm tra lại!!");
                            Console.ResetColor();
                        }
                    }
                    while (newNode.data.thoiGianXP.thang > 12 || newNode.data.thoiGianXP.thang < 0 || !isInt);
                    do
                    {
                        try
                        {
                            Console.Write("Nhập năm: ");
                            newNode.data.thoiGianXP.nam = int.Parse(Console.ReadLine());
                            isInt = true;
                        }
                        catch (FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Năm có kiểu dữ liệu là kiểu số nguyên");
                            Console.ResetColor();
                        }
                        if (newNode.data.thoiGianXP.nam < 1936 || newNode.data.thoiGianXP.nam > DateTime.Now.Year + 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nhập năm không quá hiện tại 1 năm và không nhỏ hơn 1936");
                            Console.ResetColor();
                        }
                    }
                    while (!isInt || newNode.data.thoiGianXP.nam < 1936 || newNode.data.thoiGianXP.nam > DateTime.Now.Year + 1);

                    temp = DateTime.DaysInMonth(newNode.data.thoiGianXP.nam, newNode.data.thoiGianXP.thang);
                    if (newNode.data.thoiGianXP.ngay > temp || newNode.data.thoiGianXP.ngay < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Tháng " + newNode.data.thoiGianXP.thang + " năm " + newNode.data.thoiGianXP.nam + " có " + temp + " ngày");
                        Console.ResetColor();
                    }

                }
                while (newNode.data.thoiGianXP.ngay > temp || newNode.data.thoiGianXP.ngay < 0);
                if (checkDateTime(newNode.data.thoiGianXP.nam, newNode.data.thoiGianXP.thang, newNode.data.thoiGianXP.ngay, newNode.data.thoiGianXP.gio, newNode.data.thoiGianXP.phut) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Thời gian khởi hành vừa nhập đã qua. Xin kiểm tra lại");
                    Console.ResetColor();
                }
            }
            while (checkDateTime(newNode.data.thoiGianXP.nam, newNode.data.thoiGianXP.thang, newNode.data.thoiGianXP.ngay, newNode.data.thoiGianXP.gio, newNode.data.thoiGianXP.phut) == false);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Thêm chuyến bay thành công.");
            Console.ResetColor();

            NodeCB get;
            get = nodeCB;
            while (get.next != null)
            {
                get = get.next;
            }
            get.next = newNode;
        }

        public bool checkMaCBInDSCB(ref NodeCB dscb, string maCB)
        {
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (KTraChuoiTrung(maCB, get.data.maChuyenBay))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckCMNDInDSCB(ref NodeCB dscb, int cmnd, string maCB)
        {
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (KTraChuoiTrung(maCB, get.data.maChuyenBay))
                {
                    for (int i = 0; i < get.sl_Dat; i++)
                    {
                        if (cmnd == get.dsve[i].cmnd)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void ChinhSuaCB(ref NodeCB nodeCB)
        {
            string maCB;
            do
            {
                Console.Write("Nhập mã chuyến bay bạn muốn chỉnh sửa thời gian bay: ");
                maCB = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if (maCB.Length > 15 || maCB == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số kí tự tối đa của mã chuyến bay là 15 kí tự và không được để trống!");
                    Console.ResetColor();
                }
                if (checkMaCBInDSCB(ref nodeCB, maCB) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mã chuyến bay này không tồn tại!!");
                    Console.ResetColor();
                }
            }
            while (checkMaCBInDSCB(ref nodeCB, maCB) == false || maCB.Length > 15 || maCB == "");

            NodeCB get;
            for (get = nodeCB; get != null; get = get.next)
            {
                if (KTraChuoiTrung(get.data.maChuyenBay, maCB))
                {
                    if (get.data.trangThai == 0 || get.data.trangThai == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chuyến bay này đã bị hủy hoặc đã hoàn tất chuyến bay nên không thể chỉnh sửa");
                        Console.ResetColor();
                    }
                    else if (get.data.trangThai == 1 || get.data.trangThai == 2)
                    {
                        bool isInt = false;
                        Console.WriteLine("----Chỉnh sửa----");
                        do
                        {
                            do
                            {
                                try
                                {
                                    Console.Write("Nhập giờ: ");
                                    get.data.thoiGianXP.gio = int.Parse(Console.ReadLine());
                                    isInt = true;
                                }
                                catch (FormatException)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Giờ có kiểu dữ liệu là kiểu số nguyên");
                                    Console.ResetColor();
                                }
                                if (get.data.thoiGianXP.gio > 23 || get.data.thoiGianXP.gio < 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Giờ nhập từ 0 -> 23!!");
                                    Console.ResetColor();
                                }
                            }
                            while (get.data.thoiGianXP.gio > 23 || get.data.thoiGianXP.gio < 0 || !isInt);

                            do
                            {
                                try
                                {
                                    Console.Write("Nhập phút: ");
                                    get.data.thoiGianXP.phut = int.Parse(Console.ReadLine());
                                    isInt = true;
                                }
                                catch (FormatException)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Phút có kiểu dữ liệu là kiểu số nguyên");
                                    Console.ResetColor();
                                }

                                if (get.data.thoiGianXP.phut > 59 || get.data.thoiGianXP.phut < 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Phút nhập từ 0 -> 59!!");
                                    Console.ResetColor();
                                }
                            }
                            while (get.data.thoiGianXP.phut > 59 || get.data.thoiGianXP.phut < 0 || !isInt);
                            int temp;
                            do
                            {
                                do
                                {
                                    try
                                    {
                                        Console.Write("Nhập ngày: ");
                                        get.data.thoiGianXP.ngay = int.Parse(Console.ReadLine());
                                        isInt = true;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Ngày có kiểu dữ liệu là kiểu số nguyên");
                                        Console.ResetColor();
                                    }
                                }
                                while (!isInt);
                                do
                                {
                                    try
                                    {
                                        Console.Write("Nhập tháng: ");
                                        get.data.thoiGianXP.thang = int.Parse(Console.ReadLine());
                                        isInt = true;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Tháng có kiểu dữ liệu là kiểu số nguyên");
                                        Console.ResetColor();
                                    }

                                    if (get.data.thoiGianXP.thang > 12 || get.data.thoiGianXP.thang < 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Trong năm có 12 tháng. Xin kiểm tra lại!!");
                                        Console.ResetColor();
                                    }
                                }
                                while (get.data.thoiGianXP.thang > 12 || get.data.thoiGianXP.thang < 0 || !isInt);
                                do
                                {
                                    try
                                    {
                                        Console.Write("Nhập năm: ");
                                        get.data.thoiGianXP.nam = int.Parse(Console.ReadLine());
                                        isInt = true;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Năm có kiểu dữ liệu là kiểu số nguyên");
                                        Console.ResetColor();
                                    }
                                    if (get.data.thoiGianXP.nam < 1936 || get.data.thoiGianXP.nam > DateTime.Now.Year + 1)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Nhập năm không quá hiện tại 1 năm và không nhỏ hơn 1936");
                                        Console.ResetColor();
                                    }
                                }
                                while (!isInt || get.data.thoiGianXP.nam < 1936 || get.data.thoiGianXP.nam > DateTime.Now.Year + 1);

                                temp = DateTime.DaysInMonth(get.data.thoiGianXP.nam, get.data.thoiGianXP.thang);
                                if (get.data.thoiGianXP.ngay > temp || get.data.thoiGianXP.ngay < 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Tháng " + get.data.thoiGianXP.thang + " năm " + get.data.thoiGianXP.nam + " có " + temp + " ngày");
                                    Console.ResetColor();
                                }

                            }
                            while (get.data.thoiGianXP.ngay > temp || get.data.thoiGianXP.ngay < 0);
                            if (checkDateTime(get.data.thoiGianXP.nam, get.data.thoiGianXP.thang, get.data.thoiGianXP.ngay, get.data.thoiGianXP.gio, get.data.thoiGianXP.phut) == false)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Thời gian khởi hành vừa nhập đã qua. Xin kiểm tra lại");
                                Console.ResetColor();
                            }
                        }
                        while (checkDateTime(get.data.thoiGianXP.nam, get.data.thoiGianXP.thang, get.data.thoiGianXP.ngay, get.data.thoiGianXP.gio, get.data.thoiGianXP.phut) == false);
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Chỉnh sửa dữ liệu chuyến bay thành công");
                    Console.ResetColor();
                    break;
                }
            }
        }

        public void HuyCB(ref NodeCB nodeCB)
        {
            string maCB;
            do
            {
                Console.Write("Nhập mã chuyến bay bạn muốn chỉnh sửa thời gian bay: ");
                maCB = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if (checkMaCBInDSCB(ref nodeCB, maCB) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mã chuyến bay này không tồn tại!!");
                    Console.ResetColor();
                }
                if (maCB.Length > 15 || maCB == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số kí tự tối đa của mã chuyến bay là 15 và không được để trống");
                    Console.ResetColor();
                }
            }
            while (checkMaCBInDSCB(ref nodeCB, maCB) == false || maCB.Length > 15 || maCB == "");
            NodeCB get;
            for (get = nodeCB; get != null; get = get.next)
            {
                if (KTraChuoiTrung(get.data.maChuyenBay, maCB))
                {
                    if (get.data.trangThai != 0 || get.data.trangThai != 3)
                    {
                        get.data.trangThai = 0;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chuyến bay đã bị hủy hoặc đã hoàn tất chuyến bay!!!");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Hủy chuyến bay thành công!!");
                    Console.ResetColor();
                    break;
                }
            }
        }

        public void DatVeKH(ref NodeCB dscb, string maCB, int gheSo, int cmnd)
        {
            NodeCB get;
            for (get = dscb; get.data.maChuyenBay != maCB; get = get.next) ;
            get.dsve[get.sl_Dat] = new Ve();
            get.dsve[get.sl_Dat].cmnd = cmnd;
            get.dsve[get.sl_Dat].gheSo = gheSo;
            get.dsve[get.sl_Dat].trangThai = true;
            get.sl_Dat++;

        }

        public bool KTraTTMaCB(ref NodeCB dscb, string maCB)
        {
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (KTraChuoiTrung(maCB, get.data.maChuyenBay))
                {
                    if (get.data.trangThai == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool KTraTrungSoGhe(ref NodeCB dscb, int soGhe, string maCB)
        {
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (KTraChuoiTrung(maCB, get.data.maChuyenBay))
                {
                    for (int i = 0; i < get.sl_Dat; i++)
                    {
                        if (get.dsve[i].gheSo == soGhe)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
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

        public int sl_Ve(ref DSMayBay dsmb, string soHieuMB)
        {
            int sl = 0;
            for (int i = 0; i < dsmb.so_MayBay; i++)
            {
                if (KTraChuoiTrung(dsmb.data[i].soHieuMayBay, soHieuMB))
                {
                    sl = dsmb.data[i].soCho;
                    break;
                }
            }
            return sl;
        }

        public string soHieu(ref NodeCB dscb, string maCB)
        {
            string s = "";
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (KTraChuoiTrung(get.data.maChuyenBay, maCB))
                {
                    s = get.data.soHieuMB;
                    break;
                }
            }
            return s;
        }

        public void DatVe(ref NodeCB dscb)
        {
            NodeKH nodeKH = new NodeKH();
            QuanLyHanhKhach.QuanLyHanhKhach dskh = new QuanLyHanhKhach.QuanLyHanhKhach();
            DSMayBay dsmb = new DSMayBay();
            QuanLyMayBay.QuanLyMayBay qlmb = new QuanLyMayBay.QuanLyMayBay();
            qlmb.LoadFileMB(ref dsmb);
            dskh.LoadFileKH(ref nodeKH);
            string maCB;
            do
            {
                Console.Write("Nhập mã chuyến bay bạn muốn đặt vé: ");
                maCB = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if (KTraTTMaCB(ref dscb, maCB) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mã chuyến bay bạn nhập không tồn tại, đã hết vé hoặc đã bị hủy");
                    Console.ResetColor();
                }
                if (maCB.Length > 15 || maCB == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số kí tự tối đa của mã chuyến bay là 15 và không được để trống!");
                    Console.ResetColor();
                }
            }
            while (KTraTTMaCB(ref dscb, maCB) == false || maCB.Length > 15 || maCB == "");

            int cmnd;
            do
            {
                Console.Write("Nhập số CMND: ");
                cmnd = nhapSo();

                if (cmnd.ToString().Length != 9)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Độ dài hợp lệ của CMND là 9!!!");
                    Console.ResetColor();
                }
                if (CheckCMNDInDSCB(ref dscb, cmnd, maCB) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số CMND này đã tồn tại");
                    Console.ResetColor();
                }
            }
            while (CheckCMNDInDSCB(ref dscb, cmnd, maCB) == true || cmnd.ToString().Length != 9);
            int soGhe;
            string soHieuMB = soHieu(ref dscb, maCB);
            int sl = sl_Ve(ref dsmb, soHieuMB);
            do
            {
                Console.Write("Nhập số ghế: ");
                soGhe = nhapSo();
                if (KTraTrungSoGhe(ref dscb, soGhe, maCB))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ghế số " + soGhe + " đã có hành khách đặt vé. Xin kiểm tra lại!!");
                    Console.ResetColor();
                }
                if (soGhe > sl || soGhe <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chuyến bay này chỉ có " + sl + " ghế ngồi. Chọn ghế từ 1 -> " + sl);
                    Console.ResetColor();
                }
            }
            while (KTraTrungSoGhe(ref dscb, soGhe, maCB) || soGhe > sl || soGhe <= 0);
            DatVeKH(ref dscb, maCB, soGhe, cmnd);
            NodeKH temp = dskh.DuyetCayByCMND(cmnd, ref nodeKH);
            if (temp.data.cmnd == cmnd)
            {
                Console.WriteLine("Họ tên: " + temp.data.ho + " " + temp.data.ten);
                if (temp.data.phai)
                {
                    Console.WriteLine("Giới tính: NAM");
                }
                else
                {
                    Console.WriteLine("Giới tính: NU");
                }
                Console.WriteLine("CMND: " + temp.data.cmnd);
            }
            else
            {
                string ho;
                do
                {
                    Console.Write("Nhập họ: "); ho = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                    if (!KTraSoTrongChuoi(ho) || ho == "")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Không để trống hay nhập số, kí tự đặc biệt trong họ và tên!!");
                        Console.ResetColor();
                    }
                }
                while (!KTraSoTrongChuoi(ho) || ho == "");
                string ten;
                do
                {
                    Console.Write("Nhập tên: "); ten = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                    if (!KTraSoTrongChuoi(ten) || ten == "")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Không để trống hay nhập số, kí tự đặc biệt trong họ và tên!!");
                        Console.ResetColor();
                    }
                }
                while (!KTraSoTrongChuoi(ten) || ten == "");

                string phai;
                do
                {
                    Console.Write("Nhập giới tính: ");
                    phai = Console.ReadLine();
                    phai = ChuoiChuanHoa(ChuoiVietHoa(phai));
                    if (phai != "NAM" && phai != "NU")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Phái chỉ được nhập NAM hoặc NU!!");
                        Console.ResetColor();
                    }
                }
                while (phai != "NAM" && phai != "NU");
                bool checkPhai = false;
                if (phai == "NAM")
                {
                    checkPhai = true;
                }
                dskh.ThemKH(ref nodeKH, cmnd, ho, ten, checkPhai);
            }
            dskh.SaveFileKH(ref nodeKH);
        }

        public bool KiemTraMaCBInDSMB(ref NodeCB dscb, string maCB)
        {
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (KTraChuoiTrung(maCB, get.data.maChuyenBay))
                {
                    return true;
                }
            }
            return false;
        }

        public void InDSKHTrenChuyenBay(ref NodeCB dscb)
        {
            StreamWriter f = new StreamWriter("D:\\Ctdl&gt\\LabCuoiKy\\DanhSachHanhKhackTrenChuyenBay.txt");

            if (f == null)
            {
                f.WriteLine("0");
            }
            NodeKH nodeKH = new NodeKH();
            QuanLyHanhKhach.QuanLyHanhKhach dskh = new QuanLyHanhKhach.QuanLyHanhKhach();
            dskh.LoadFileKH(ref nodeKH);
            NodeCB get;
            String maCB;
            do
            {
                Console.Write("Nhập mã chuyến bay bạn muốn in danh sách: ");
                maCB = Console.ReadLine();
                maCB = ChuoiChuanHoa(ChuoiVietHoa(maCB));
                if (KiemTraMaCBInDSMB(ref dscb, maCB) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mã chuyến bay này không tồn tại!!!");
                    Console.ResetColor();
                }
                if (maCB.Length > 15 || maCB == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Số kí tự tối đa của mã chuyến bay là 15 kí tự!");
                    Console.ResetColor();
                }
            }
            while (KiemTraMaCBInDSMB(ref dscb, maCB) == false || maCB.Length > 15 || maCB == "");
            for (get = dscb; get != null; get = get.next)
            {
                if (KTraChuoiTrung(maCB, get.data.maChuyenBay))
                {
                    Console.WriteLine("Danh sách khách hàng trên chuyến bay " + get.data.maChuyenBay);
                    Console.Write("Ngày giờ khởi hành: " + get.data.thoiGianXP.ngay + "/" + get.data.thoiGianXP.thang + "/" + get.data.thoiGianXP.nam + " " + get.data.thoiGianXP.gio + ":" + get.data.thoiGianXP.phut);
                    Console.WriteLine("\tNơi đến: " + get.data.noiDen);
                    Console.Write("STT");
                    Console.Write("\tSố ghế");
                    Console.Write("\tCMND");
                    Console.Write("\t\tPhái");
                    Console.WriteLine("\tHọ tên");
                    f.WriteLine("Danh sách khách hàng trên chuyến bay " + get.data.maChuyenBay);
                    f.Write("Ngày giờ khởi hành: " + get.data.thoiGianXP.ngay + "/" + get.data.thoiGianXP.thang + "/" + get.data.thoiGianXP.nam + " " + get.data.thoiGianXP.gio + ":" + get.data.thoiGianXP.phut);
                    f.WriteLine("\tNơi đến: " + get.data.noiDen);
                    f.Write("STT");
                    f.Write("\tSố ghế");
                    f.Write("\tCMND");
                    f.Write("\tPhái");
                    f.WriteLine("\tHọ tên");
                    for (int i = 0; i < get.sl_Dat; i++)
                    {
                        Console.Write(i + 1);
                        f.Write(i + 1);
                        Console.Write("\t" + get.dsve[i].gheSo);
                        f.Write("\t" + get.dsve[i].gheSo);
                        Console.Write("\t" + get.dsve[i].cmnd);
                        f.Write("\t" + get.dsve[i].cmnd);
                        NodeKH temp = dskh.DuyetCayByCMND(get.dsve[i].cmnd, ref nodeKH);
                        if (temp.data.cmnd == get.dsve[i].cmnd)
                        {

                            if (temp.data.phai)
                            {
                                Console.Write("\t NAM");
                                f.Write("\t NAM");
                            }
                            else
                            {
                                Console.Write("\t NU");
                                f.Write("\t NU");
                            }
                            Console.Write("\t" + temp.data.ho + " " + temp.data.ten);
                            f.Write("\t" + temp.data.ho + " " + temp.data.ten);
                        }
                        Console.WriteLine();
                        f.WriteLine();
                    }
                }
            }
            f.Close();
        }        

        public bool checkNoiDen(ref NodeCB dscb, string noiDen)
        {
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (KTraChuoiTrung(noiDen, get.data.noiDen))
                {
                    return true;
                }
            }
            return false;
        }

        public void InTatCaDSCB(ref NodeCB dscb)
        {
            Console.WriteLine("Danh sách tất cả chuyến bay");
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                Console.WriteLine("Danh sách chuyến bay " + get.data.maChuyenBay);
                Console.Write("Ngày giờ khởi hành: " + get.data.thoiGianXP.ngay + "/" + get.data.thoiGianXP.thang + "/" + get.data.thoiGianXP.nam + " " + get.data.thoiGianXP.gio + ":" + get.data.thoiGianXP.phut);
                Console.Write("\tNơi đến: " + get.data.noiDen);
                Console.WriteLine("\tTrạng thái chuyến bay: " + get.data.trangThai);
                Console.WriteLine("Tổng số ghế: " + get.sl_Ve);
                if (get.sl_Dat == 0)
                {
                    Console.WriteLine("Số ghế còn trống: " + get.sl_Ve);
                }
                else
                {
                    Console.WriteLine("Số ghế còn trống: " + (get.sl_Ve - get.sl_Dat));
                }
            }
        }

        public void InDSCBTheoNoiDenTrongNgay(ref NodeCB dscb)
        {
            StreamWriter f = new StreamWriter("D:\\Ctdl&gt\\LabCuoiKy\\DanhSachChuyenBayDenDiaDiemTrongNgay.txt");

            if (f == null)
            {
                f.WriteLine("0");
            }

            string noiDen;
            do
            {
                Console.Write("Nhập nơi đến: ");
                noiDen = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if (checkNoiDen(ref dscb, noiDen) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nơi đến này không có trong danh sách chuyến bay!!");
                    Console.ResetColor();
                }
                if (noiDen == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nơi đến không được để trống!");
                    Console.ResetColor();
                }
            }
            while (checkNoiDen(ref dscb, noiDen) == false || noiDen == "");

            int ngay, thang, nam;
            do
            {
                Console.Write("Nhập ngày: ");
                ngay = nhapSo();
                do
                {
                    Console.Write("Nhập tháng: ");
                    thang = nhapSo();
                    if (thang < 0 || thang > 12)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nhập tháng không hợp lệ!!!");
                        Console.ResetColor();
                    }
                }
                while (thang < 0 || thang > 12);

                do
                {
                    Console.Write("Nhập năm: ");
                    nam = nhapSo();
                    if (nam < 1936 || nam > DateTime.Now.Year + 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nhập năm không quá hiện tại 1 năm và không nhỏ hơn 1936!!");
                        Console.ResetColor();
                    }
                }
                while (nam < 1936 || nam > DateTime.Now.Year + 1);  //máy bay chở khách thương mại đầu tiên được sản xuất là Douglas DC-3 vào năm 1936
                if (ngay < 0 || ngay > DateTime.DaysInMonth(nam, thang))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tháng " + thang + "/" + nam + " có " + DateTime.DaysInMonth(nam, thang) + " ngày");
                    Console.ResetColor();
                }
            }
            while (ngay < 0 || ngay > DateTime.DaysInMonth(nam, thang));
            Console.WriteLine("Danh sách chuyến bay đến " + noiDen + " ngày " + ngay + "/" + thang + "/" + nam);
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (get.data.trangThai != 0)
                {
                    if (KTraChuoiTrung(noiDen, get.data.noiDen))
                    {
                        if (ngay == get.data.thoiGianXP.ngay && thang == get.data.thoiGianXP.thang && nam == get.data.thoiGianXP.nam)
                        {
                            Console.WriteLine("Danh sách chuyến bay " + get.data.maChuyenBay);
                            Console.Write("Ngày giờ khởi hành: " + get.data.thoiGianXP.ngay + "/" + get.data.thoiGianXP.thang + "/" + get.data.thoiGianXP.nam + " " + get.data.thoiGianXP.gio + ":" + get.data.thoiGianXP.phut);
                            Console.WriteLine("\tNơi đến: " + get.data.noiDen);

                            f.WriteLine("Danh sách chuyến bay " + get.data.maChuyenBay);
                            f.Write("Ngày giờ khởi hành: " + get.data.thoiGianXP.ngay + "/" + get.data.thoiGianXP.thang + "/" + get.data.thoiGianXP.nam + " " + get.data.thoiGianXP.gio + ":" + get.data.thoiGianXP.phut);
                            f.WriteLine("\tNơi đến: " + get.data.noiDen);

                            Console.WriteLine("Tổng số ghế: " + get.sl_Ve);
                            f.WriteLine("Tổng số ghế: " + get.sl_Ve);
                            if (get.sl_Dat == 0)
                            {
                                Console.WriteLine("Số ghế còn trống: " + get.sl_Ve);
                                f.WriteLine("Số ghế còn trống: " + get.sl_Ve);
                            }
                            else
                            {
                                Console.Write("Các ghế đã được đặt: ");
                                f.Write("Các ghế đã được đặt: ");
                                for (int i = 0; i < get.sl_Dat; i++)
                                {
                                    Console.Write(get.dsve[i].gheSo + "  ");
                                    f.Write(get.dsve[i].gheSo + "  ");
                                }
                                Console.WriteLine();
                                Console.WriteLine("Số ghế còn trống: " + (get.sl_Ve - get.sl_Dat));
                                f.WriteLine();
                                f.WriteLine("Số ghế còn trống: " + (get.sl_Ve - get.sl_Dat));
                            }
                        }
                    }
                    else
                    {
                        if (get.next == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Không có chuyến bay đến " + noiDen + " trong ngày: " + ngay + "/" + thang + "/" + nam);
                            Console.ResetColor();
                        }
                    }
                }
            }
            f.Close();
        }

        public void InDSVeTrong(ref NodeCB dscb)
        {
            StreamWriter f = new StreamWriter("D:\\Ctdl&gt\\LabCuoiKy\\DanhSachVeTrong.txt");

            if (f == null)
            {
                f.WriteLine("0");
            }

            string maCB;
            do
            {
                Console.Write("Nhập mã chuyến bay: ");
                maCB = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                if (checkMaCBInDSCB(ref dscb, maCB) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mã chuyến này không tồn tại");
                    Console.ResetColor();
                }
            }
            while (checkMaCBInDSCB(ref dscb, maCB) == false || maCB.Length > 15 || maCB == "");
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (KTraChuoiTrung(get.data.maChuyenBay, maCB))
                {
                    Console.WriteLine("Danh sách vé trống của chuyến bay " + get.data.maChuyenBay);
                    f.WriteLine("Danh sách khách hàng trên chuyến bay " + get.data.maChuyenBay);
                    Console.Write("Ngày giờ khởi hành: " + get.data.thoiGianXP.ngay + "/" + get.data.thoiGianXP.thang + "/" + get.data.thoiGianXP.nam + " " + get.data.thoiGianXP.gio + ":" + get.data.thoiGianXP.phut);
                    f.Write("Ngày giờ khởi hành: " + get.data.thoiGianXP.ngay + "/" + get.data.thoiGianXP.thang + "/" + get.data.thoiGianXP.nam + " " + get.data.thoiGianXP.gio + ":" + get.data.thoiGianXP.phut);
                    Console.WriteLine("\tNơi đến: " + get.data.noiDen);
                    f.WriteLine("\tNơi đến: " + get.data.noiDen);

                    Console.WriteLine("Tổng số ghế: " + get.sl_Ve);
                    f.WriteLine("Tổng số ghế: " + get.sl_Ve);
                    if (get.sl_Dat == 0)
                    {
                        Console.WriteLine("Số ghế còn trống: " + get.sl_Ve);
                        f.WriteLine("Số ghế còn trống: " + get.sl_Ve);
                    }
                    else
                    {
                        Console.WriteLine("Số ghế còn trống: " + (get.sl_Ve - get.sl_Dat));
                        f.WriteLine("Số ghế còn trống: " + (get.sl_Ve - get.sl_Dat));
                        Console.Write("Danh sách ghế còn trống: ");
                        f.Write("Danh sách ghế còn trống: ");

                        for (int i = 1; i <= get.sl_Ve; i++)
                        {
                            bool found = false;
                            for (int j = 0; j < get.sl_Dat; j++)
                            {
                                if (get.dsve[j].gheSo == i)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                Console.Write(i + "  ");
                                f.Write(i + "  ");
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
            f.Close();
        }

        public int TrangThaiCB(ref NodeCB dscb, string maCB)
        {
            int kq = 1;
            DateTime now = DateTime.Now;
            int nam = now.Year;
            int thang = now.Month;
            int ngay = now.Day;
            int gio = now.Hour;
            int phut = now.Minute;
            NodeCB get;
            for (get = dscb; get != null; get = get.next)
            {
                if (get.data.trangThai == 0)
                {
                    kq = 0;
                    break;
                }
                else
                {
                    if (KTraChuoiTrung(maCB, get.data.maChuyenBay))
                    {
                        if (get.sl_Dat == get.sl_Ve)
                        {
                            kq = 2;
                        }
                        else if (checkDateTime(get.data.thoiGianXP.nam, get.data.thoiGianXP.thang, get.data.thoiGianXP.ngay, get.data.thoiGianXP.gio, get.data.thoiGianXP.phut) == false)
                        {
                            kq = 3;
                        }
                        break;
                    }
                }
            }
            return kq;
        }

        public int LoadFileCB(ref NodeCB dscb)
        {
            StreamReader f = new StreamReader("D:\\Ctdl&gt\\LabCuoiKy\\dscb.txt");
            if (f == null) return 0;
            int slcb;
            int.TryParse(f.ReadLine(), out slcb);
            if (slcb == 0)
            {
                dscb = null;
                return 0;
            }
            int i;
            NodeCB ds;
            f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine();
            f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine();
            for (i = 0; i < slcb; i++)
            {
                ds = new NodeCB();
                int.TryParse(f.ReadLine(), out ds.sl_Dat);
                int.TryParse(f.ReadLine(), out ds.sl_Ve);
                ds.data.maChuyenBay = f.ReadLine();
                ds.data.noiDen = f.ReadLine();
                ds.data.soHieuMB = f.ReadLine();
                int.TryParse(f.ReadLine(), out ds.data.trangThai);
                int.TryParse(f.ReadLine(), out ds.data.thoiGianXP.gio);
                int.TryParse(f.ReadLine(), out ds.data.thoiGianXP.phut);
                int.TryParse(f.ReadLine(), out ds.data.thoiGianXP.ngay);
                int.TryParse(f.ReadLine(), out ds.data.thoiGianXP.thang);
                int.TryParse(f.ReadLine(), out ds.data.thoiGianXP.nam);
                for (int j = 0; j < ds.sl_Dat; j++)
                {
                    f.ReadLine();
                    ds.dsve[j] = new Ve();
                    int.TryParse(f.ReadLine(), out ds.dsve[j].cmnd);
                    int.TryParse(f.ReadLine(), out ds.dsve[j].gheSo);
                }
                f.ReadLine();
                NodeCB get;
                if (i == 0)
                {
                    dscb = ds;
                }
                else
                {
                    get = dscb;
                    while (get.next != null)
                    {
                        get = get.next;
                    }
                    get.next = ds;
                }
            }
            f.Close();
            return 1;
        }

        public int SaveFileCB(ref NodeCB ds)
        {
            StreamWriter f = new StreamWriter("D:\\Ctdl&gt\\LabCuoiKy\\dscb.txt");

            if (f == null) return 0;
            NodeCB get;
            int slcb = ktslcb(ds);

            f.WriteLine(slcb);
            f.WriteLine("------------------------------------");
            f.WriteLine("Số lượng đặt vé");
            f.WriteLine("Số lượng vé");
            f.WriteLine("Mã chuyến bay");
            f.WriteLine("Nơi đến");
            f.WriteLine("Số hiệu máy bay");
            f.WriteLine("Trạng thái chuyến bay(0: Hủy chuyến, 1: Còn vé, 2: Hết vé, 3: Hoàn tất)");
            f.WriteLine("Giờ");
            f.WriteLine("Phút");
            f.WriteLine("Ngày");
            f.WriteLine("Tháng");
            f.WriteLine("Năm");
            f.WriteLine("..................");
            f.WriteLine("CMND");
            f.WriteLine("Số ghế ngồi");
            f.WriteLine("------------------------------------");

            for (get = ds; get != null; get = get.next)
            {
                f.WriteLine(get.sl_Dat);
                f.WriteLine(get.sl_Ve);
                f.WriteLine(get.data.maChuyenBay);
                f.WriteLine(get.data.noiDen);
                f.WriteLine(get.data.soHieuMB);
                get.data.trangThai = TrangThaiCB(ref get, get.data.maChuyenBay);
                f.WriteLine(get.data.trangThai);
                f.WriteLine(get.data.thoiGianXP.gio);
                f.WriteLine(get.data.thoiGianXP.phut);
                f.WriteLine(get.data.thoiGianXP.ngay);
                f.WriteLine(get.data.thoiGianXP.thang);
                f.WriteLine(get.data.thoiGianXP.nam);

                for (int i = 0; i < get.sl_Dat; i++)
                {
                    f.WriteLine(".............");
                    f.WriteLine(get.dsve[i].cmnd);
                    f.WriteLine(get.dsve[i].gheSo);
                }
                f.WriteLine("------------------------------------");
            }
            f.Close();
            return 1;
        }
    }
}