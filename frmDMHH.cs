using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LTUDMIS62
{
    public partial class frmDMHH : Form
    {
        SqlConnection conn= new SqlConnection();
        SqlDataAdapter da=new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt=new DataTable();
        DataTable comdt=new DataTable();
        string sql, constr;
        int i;
        Boolean addnewflag = false;
        public frmDMHH()
        {
            InitializeComponent();
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmDMHH_Load(object sender, EventArgs e)
        {
            constr = "Data Source = MSI\\SQLEXPRESS; Initial Catalog = QLBH;" +
                "Integrated Security = True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "select Manhom,MaHH,TenHH,Dvt,Dgvnd, Sanxuat from tblDMHH order by MaHH";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdData.DataSource = dt;
            NapCT();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            i = grdData.CurrentRow.Index;
            if (i > 0 )
            {
                grdData.CurrentCell = grdData[0, i-1];
            }
            else
            {
                grdData.CurrentCell = grdData[0, grdData.RowCount -2];
            }
            NapCT();
        }

            private void btnFirst_Click(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0,0];//Cột 0 và dòng 0
            NapCT(); 
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            i = grdData.Rows.Count;//Có bnh bản ghi
            grdData.CurrentCell = grdData[0,i-2];//Dòng 0, cột i-2, là i-2 vì dữ liệu chạy từ 0
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            i = grdData.CurrentRow.Index;
            if (i <= grdData.RowCount - 2)
            {
                grdData.CurrentCell = grdData[0, i + 1];
            }
            else
            {
                grdData.CurrentCell = grdData[0,0];
            }    
            NapCT();
        }

        private void txtTenHH_Click(object sender, EventArgs e)
        {

        }

        private void txtDvt_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            sql = "select Manhom,MaHH,TenHH,Dvt,Dgvnd, Sanxuat from tblDMHH order by MaHH";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
            NapCT();
        }

        private void comTenTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            sql = "select Distinct " + comTenTruong.Text + " from tblDMHH ";
            da = new SqlDataAdapter(sql, conn);
            comdt.Clear();
            da.Fill(comdt);
            comGiaTri.DataSource = comdt;
            comGiaTri.DisplayMember = comTenTruong.Text;
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
           
            sql = "select Manhom,MaHH,TenHH,Dvt,Dgvnd, Sanxuat from tblDMHH "+
                "where " + comTenTruong.Text + "=N'" + comGiaTri.Text + "'";//Lồng điều kiện vào
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
            NapCT();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDL_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi hiện thời ?Y/N ", "Xác nhận" +
                " yêu cầu", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)==DialogResult.Yes)
            {
                sql = "Delete from tblDMHH where MaHH='" + txtMaHH.Text + "'";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                grdData.Rows.RemoveAt(grdData.CurrentRow.Index);
                NapCT();
            }    
        }

        private void btnEX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hãy thực hiện mọi sửa đổi mong muốn trên ô lưới, kết thúc bấm nút Cập nhật", "Thông báo", MessageBoxButtons.OK);
            btnUD.Enabled = true;
        }

        private void btnUD_Click(object sender, EventArgs e)
        {
            if (addnewflag==false)
            {
                //chỗ này là cập nhật sửa chữa

                for (int i = 0; i < grdData.Rows.Count - 1; i++)
                {
                    txtManhom.Text = grdData.Rows[i].Cells["Manhom"].Value.ToString();
                    txtMaHH.Text = grdData.Rows[i].Cells["MaHH"].Value.ToString();
                    txtTenHH.Text = grdData.Rows[i].Cells["TenHH"].Value.ToString();
                    txtDvt.Text = grdData.Rows[i].Cells["Dvt"].Value.ToString();
                    txtDgvnd.Text = grdData.Rows[i].Cells["Dgvnd"].Value.ToString();
                    txtSanxuat.Text = grdData.Rows[i].Cells["Sanxuat"].Value.ToString();
                    sql = "Update tblDMHH set dgVnd=" + txtDgvnd.Text + "," + 
                        " TenHH=N'" + txtTenHH.Text + "', Dvt=N'" + txtDvt.Text + "'," + "Sanxuat=N'" +
                        txtSanxuat.Text + "' Where MaHH='" + txtMaHH.Text + "'";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();  
                }
                MessageBox.Show("Đã cập nhật thành công!", "Thông báo");
            }
            else
            {
                //chỗ này cập nhật trên mới
                addnewflag = false;
                sql = "insert into tblDMHH (MaNhom, MaHH, TenHH, Dvt, Dgvnd, Sanxuat)" +
                    " Values ('" + txtManhom.Text + "','" + txtMaHH.Text + "',N'" +
                    txtTenHH.Text + "',N'" + txtDvt.Text + "'," + txtDgvnd.Text + ",N'" +
                    txtSanxuat.Text + "')";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                grdData.Rows[i].Cells["Manhom"].Value=txtManhom.Text;
                grdData.Rows[i].Cells["MaHH"].Value = txtMaHH.Text;
                grdData.Rows[i].Cells["TenHH"].Value = txtTenHH.Text;
                grdData.Rows[i].Cells["Dvt"].Value = txtDvt.Text;
                grdData.Rows[i].Cells["Dgvnd"].Value = txtDgvnd.Text;
                grdData.Rows[i].Cells["Sanxuat"].Value = txtSanxuat.Text;
                grdData.Refresh();

            }
            btnUD.Enabled = false;
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            sql = "select Manhom,MaHH,TenHH,Dvt,Dgvnd, Sanxuat from tblDMHH order by MaHH";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdData.DataSource = dt;
            NapCT();

            grdData.CurrentCell = grdData[0, grdData.RowCount - 1]; //di chuyển bản ghi hiện thời tới dòng cuối cùng
            NapCT();
            MessageBox.Show("Hãy nhập nội dung bản ghi mới, kết thúc bấm Cập nhật!");
            txtManhom.Focus();// chuyển con trỏ soạn thảo tới txtManhom
            addnewflag=true;
            btnUD.Enabled = true;
        }

        private void NapCT()
        {
            i = grdData.CurrentRow.Index;
           txtManhom.Text = grdData.Rows[i].Cells["Manhom"].Value.ToString();
            txtMaHH.Text = grdData.Rows[i].Cells["MaHH"].Value.ToString();
            txtTenHH.Text = grdData.Rows[i].Cells["TenHH"].Value.ToString();
            txtDvt.Text = grdData.Rows[i].Cells["Dvt"].Value.ToString();
            txtDgvnd.Text = grdData.Rows[i].Cells["Dgvnd"].Value.ToString();
            txtSanxuat.Text = grdData.Rows[i].Cells["Sanxuat"].Value.ToString();
        }
    }
}
