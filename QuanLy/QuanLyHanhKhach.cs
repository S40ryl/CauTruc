using System;
using System.IO;
using System.Text;
using ThuVien;

namespace QuanLyHanhKhach
{
    public class QuanLyHanhKhach
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
                        return false;
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

        public void getslKH(ref NodeKH dskh, ref int kq)
        {
            if (dskh.data.cmnd != 0)
            {
                kq++;
                getslKH(ref dskh.left, ref kq);
                getslKH(ref dskh.right, ref kq);
            }
        }

        public void ThemKH(ref NodeKH nodeKH, int cmnd, string ho, string ten, bool phai)
        {
            if (nodeKH.data.cmnd == 0)
            {
                NodeKH p = new NodeKH();
                p.data.cmnd = cmnd;
                p.data.ho = ho;
                p.data.ten = ten;
                p.data.phai = phai;
                p.left = new NodeKH();
                p.right = new NodeKH();
                nodeKH = p;
            }
            else
            {
                if (nodeKH.data.cmnd > cmnd)
                {
                    ThemKH(ref nodeKH.left, cmnd, ho, ten, phai);
                }
                else if (nodeKH.data.cmnd < cmnd)
                {
                    ThemKH(ref nodeKH.right, cmnd, ho, ten, phai);
                }
            }
        }

        public void xuatkh(NodeKH dskh, ref int chiso, ref int[] cmnd, ref string[] ho, ref string[] ten, ref bool[] phai)
        {
            if (dskh.data.cmnd != 0)
            {
                cmnd[chiso] = dskh.data.cmnd;
                ho[chiso] = dskh.data.ho;
                ten[chiso] = dskh.data.ten;
                phai[chiso] = dskh.data.phai;
                chiso++;
                xuatkh(dskh.left, ref chiso, ref cmnd, ref ho, ref ten, ref phai);
                xuatkh(dskh.right, ref chiso, ref cmnd, ref ho, ref ten, ref phai);
            }
        }

        public bool checkCMND(ref NodeKH kh, int cmnd)
        {
            if (kh.data.cmnd == 0) return false;
            else if (kh.data.cmnd == cmnd) return true;
            else if (kh.data.cmnd > cmnd) return (checkCMND(ref kh.left, cmnd));
            else if (kh.data.cmnd < cmnd) return (checkCMND(ref kh.right, cmnd));
            return false;
        }

        public void NhapHK(ref NodeKH dshk)
        {
            int cmnd;
            bool isInt = false;
            try
            {
                do
                {
                    Console.Write("Nhập CMND của hành khách: ");
                    cmnd = int.Parse(Console.ReadLine());
                    if (checkCMND(ref dshk, cmnd))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("CMND này đã tồn tại!!");
                        Console.ResetColor();
                    }
                } while (checkCMND(ref dshk, cmnd) || !isInt);
                Console.Write("Nhập họ: "); string ho = Console.ReadLine();
                Console.Write("Nhập tên: "); string ten = Console.ReadLine();
                string phai;
                do
                {
                    Console.Write("Nhập giới tính: "); phai = ChuoiChuanHoa(ChuoiVietHoa(Console.ReadLine()));
                    if (phai != "NAM" || phai != "NU")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Phái chỉ được nhập NAM hoặc NU!!");
                        Console.ResetColor();
                    }
                }
                while (phai != "NAM" || phai != "NU");
                bool temp = false;
                if (KTraChuoiTrung(phai, "NAM"))
                {
                    temp = true;
                }
                ThemKH(ref dshk, cmnd, ho, ten, temp);
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CMND có kiểu dữ liệu là kiểu số nguyên!");
                Console.ResetColor();
            }
        }

        public NodeKH DuyetCayByCMND(int cmnd, ref NodeKH nodeKH)
        {
            NodeKH temp = new NodeKH();
            if (nodeKH.data.cmnd == 0) return temp;
            if (nodeKH.data.cmnd == cmnd)
            {
                return nodeKH;
            }
            else
            {
                if (nodeKH.data.cmnd > cmnd)
                {
                    return DuyetCayByCMND(cmnd, ref nodeKH.left);
                }
                else if (nodeKH.data.cmnd < cmnd)
                {
                    return DuyetCayByCMND(cmnd, ref nodeKH.right);
                }
            }
            return temp;
        }

        public int InTatCaDSKH(ref NodeKH nodeKH)
        {
            LoadFileKH(ref nodeKH);
            StreamReader f = new StreamReader("D:\\Ctdl&gt\\LabCuoiKy\\dskh.txt");
            if (f == null) return 0;
            int slkh;
            int.TryParse(f.ReadLine(), out slkh);
            f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine();
            if (slkh == 0)
            {
                return 0;
            }
            int[] cmnd_t = new int[slkh];
            string[] ho_t = new string[slkh];
            string[] ten_t = new string[slkh];
            string[] phai_char = new string[slkh];

            Console.Write("STT");
            Console.Write("\tCMND");
            Console.Write("\t\tPhái");
            Console.WriteLine("\tHọ tên");
            for (int i = 0; i < slkh; i++)
            {
                Console.Write(i + 1);
                int.TryParse(f.ReadLine(), out cmnd_t[i]);
                Console.Write("\t" + cmnd_t[i]);
                ho_t[i] = f.ReadLine();
                ten_t[i] = f.ReadLine();
                phai_char[i] = f.ReadLine();
                Console.Write("\t" + phai_char[i]);
                Console.WriteLine("\t" + ho_t[i] + " " + ten_t[i]);
                f.ReadLine();
            }
            f.Close();
            return 1;
        }

        public int LoadFileKH(ref NodeKH dskh)
        {
            StreamReader f = new StreamReader("D:\\Ctdl&gt\\LabCuoiKy\\dskh.txt");
            if (f == null) return 0;
            int slkh;
            int.TryParse(f.ReadLine(), out slkh);
            f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine(); f.ReadLine();
            if (slkh == 0)
            {
                dskh = null;
                return 0;
            }
            int[] cmnd_t = new int[slkh];
            string[] ho_t = new string[slkh];
            string[] ten_t = new string[slkh];
            string[] phai_char = new string[slkh];
            bool[] phai_t = new bool[slkh];

            for (int i = 0; i < slkh; i++)
            {
                int.TryParse(f.ReadLine(), out cmnd_t[i]);
                ho_t[i] = f.ReadLine();
                ten_t[i] = f.ReadLine();
                phai_char[i] = f.ReadLine();
                phai_t[i] = KTraChuoiTrung(phai_char[i], "NAM") ? true : false;
                ThemKH(ref dskh, cmnd_t[i], ho_t[i], ten_t[i], phai_t[i]);
                f.ReadLine();
            }
            f.Close();
            return 1;
        }

        public int SaveFileKH(ref NodeKH ds)
        {
            StreamWriter f = new StreamWriter("D:\\Ctdl&gt\\LabCuoiKy\\dskh.txt");
            int slkh = 0;
            getslKH(ref ds, ref slkh);
            f.WriteLine(slkh);
            f.WriteLine("----------------------------------------");
            f.WriteLine("CMND");
            f.WriteLine("Họ");
            f.WriteLine("Tên");
            f.WriteLine("Giới tính");
            f.WriteLine("----------------------------------------");
            int[] cmnd_t = new int[slkh];
            string[] ho_t = new string[slkh];
            string[] ten_t = new string[slkh];
            bool[] phai_t = new bool[slkh];
            int chiso = 0;
            xuatkh(ds, ref chiso, ref cmnd_t, ref ho_t, ref ten_t, ref phai_t);
            for (int i = 0; i < slkh; i++)
            {
                f.WriteLine(cmnd_t[i]);
                f.WriteLine(ho_t[i]);
                f.WriteLine(ten_t[i]);
                f.WriteLine((phai_t[i] ? "NAM" : "NU"));
                f.WriteLine("----------------------------------------");
            }
            f.Close();
            return 1;
        }

    }
}
