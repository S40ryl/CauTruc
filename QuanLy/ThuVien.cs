using System;
using System.Text;
using QuanLyMayBay;
using QuanLyChuyenBay;
using QuanLyHanhKhach;

namespace ThuVien
{
    public class ThuVien
    {
        public const int MACDINH = 0;
        public const int MD_TTCB = 1;
        public const int MAX_MB = 300;
        public const int MAX_VE = 200;
    }
    public class MayBay
    {
        public string soHieuMayBay;
        public string loaiMayBay;
        public int soCho;
        public int sl_Bay = ThuVien.MACDINH;
        public bool trangThai = false;
    }

    public class DSMayBay
    {
        public MayBay[] data = new MayBay[ThuVien.MAX_MB];
        public int so_MayBay;
    }

    public class ThoiGian
    {
        public int phut;
        public int gio;
        public int ngay;
        public int thang;
        public int nam;
        public ThoiGian()
        {
            phut = ThuVien.MACDINH;
            gio = ThuVien.MACDINH;
            ngay = ThuVien.MACDINH;
            thang = ThuVien.MACDINH;
            nam = ThuVien.MACDINH;
        }
    }

    public class KhachHang
    {
        public int cmnd;
        public string ho;
        public string ten;
        public bool phai = true;  //true:nam-false:nu
        public KhachHang()
        {
            cmnd = 0;
            ho = "";
            ten = "";
        }
    }

    public class NodeKH
    {
        public KhachHang data = new KhachHang();
        public int n = ThuVien.MACDINH;
        public NodeKH left = null;
        public NodeKH right = null;
    }

    public class Ve
    {
        public int cmnd;
        public int gheSo;
        public bool trangThai = true; // true la co nguoi dat, false la ve bi huy
        public Ve()
        {
            cmnd = 0;
            gheSo = 0;
        }
    }

    public class ChuyenBay
    {
        public string maChuyenBay;
        public ThoiGian thoiGianXP;
        public string noiDen;
        public int trangThai = ThuVien.MD_TTCB;
        public string soHieuMB;
        public ChuyenBay()
        {
            maChuyenBay = "";
            noiDen = "";
            soHieuMB = "";
            thoiGianXP = new ThoiGian();
        }
    }

    public class NodeCB
    {
        public ChuyenBay data;
        public int sl_Ve;
        public int sl_Dat;
        public Ve[] dsve = new Ve[ThuVien.MAX_VE];
        public NodeCB next = null;
        public NodeCB()
        {
            sl_Dat = 0;
            sl_Ve = 0;
            data = new ChuyenBay();
        }
    }
}
