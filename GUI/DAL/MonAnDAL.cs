using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class MonAnDAL
    {
        MonAn monAn = new MonAn();

        public DataTable loadMonAn()
        {
            return monAn.loadMonAn();
        }

        public DataTable loadDanhMucMonAnCanNhap()
        {
            return monAn.loadDanhMucMonAnCanNhap();
        }

        public void themMonAn(string tenMonAn, string loai, string donViTinh, int gia)
        {
            monAn.themMonAn(tenMonAn, loai, donViTinh, gia);
        }

        public void suaGiaBan(int giaBan, int maMonAn)
        {
            monAn.suaGiaBan(giaBan, maMonAn);
        }

        public DataTable loadDSTenMonAn()
        {
            return monAn.loadDSTenMonAn();
        }

        public DataTable loadDsLoaiMonAn()
        {
            return monAn.loadDsLoaiMonAn();
        }

        public DataTable traVeDSMonAnTheoLoai(string Loai)
        {
            return monAn.traVeDSMonAnTheoLoai(Loai);
        }

        public DataTable traVeMonAnTheoMa(int maMonAn)
        {
            return monAn.traVeMonAnTheoMa(maMonAn);
        }

        public void updateLaiSoLuongTheoMaMonAn(int maMonAn)
        {
            monAn.updateLaiSoLuongTheoMaMonAn(maMonAn);
        }


        public void updateLaiSoLuongTheoMaMonAnKhiHuyGoiMon(int maMonAn)
        {
            monAn.updateLaiSoLuongTheoMaMonAnKhiHuyGoiMon(maMonAn);
        }

        public DataTable timKiemTheoMaMonAnDAL(string maMonAn)
        {
            return monAn.timKiemTheoMaMonAn(maMonAn);
        }

    }
}
