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
        public string file_mb = "datadsmaybay.txt";
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
        int phut;
        int gio;
        int ngay;
        int thang;
        int nam;
    }

    public class KhachHang
    {
        string cmnd;
        string ho;
        string ten;
        bool phai;  //true:nam-false:nu
    }

    public class NodeKH
    {
        KhachHang data;
        int n = ThuVien.MACDINH;
        NodeKH left = null;
        NodeKH right = null;
    }

    public class Ve
    {
        string cmnd;
        int gheSo;
        bool trangThai = true; // true la co nguoi dat, false la ve bi huy
    }

    public class ChuyenBay
    {
        string maChuyenBay;
        ThoiGian thoiGianXP;
        string noiDen;
        int trangThai = ThuVien.MD_TTCB;
        string soHieuMB;
    }

    public class NodeCB
    {
        ChuyenBay data;
        int sl_Ve;
        int sl_Dat;
        Ve[] dsve = new Ve[ThuVien.MAX_VE];
        NodeCB next = null;
    }


}
