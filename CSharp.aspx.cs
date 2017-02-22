using System;
using System.Data;
using System.Data.SqlClient ;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing; 

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
   
    protected void InsertDoc_Click(object sender, EventArgs e)
    {
        // Read the file and convert it to Byte Array
        string filePath = Server.MapPath("APP_DATA/TestDoc.docx");
        string filename = Path.GetFileName(filePath);

        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        br.Close();
        fs.Close();

        //insert the file into database
        string strQuery = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename;
        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = "application/vnd.ms-word";
        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
        InsertUpdateData(cmd);
    }
    protected void InsertXls_Click(object sender, EventArgs e)
    {
        // Read the file and convert it to Byte Array
        string filePath = Server.MapPath("APP_DATA/Testxls.xlsx");
        string filename = Path.GetFileName(filePath);

        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        br.Close();
        fs.Close();

        //insert the file into database
        string strQuery = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename;
        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = "application/vnd.ms-excel";
        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
        InsertUpdateData(cmd);
    }
    protected void InsertImage_Click(object sender, EventArgs e)
    {
        // Read the file and convert it to Byte Array
        string filePath = Server.MapPath("APP_DATA/TestImage.jpg");
        string filename = Path.GetFileName(filePath);

        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        br.Close();
        fs.Close();

        //insert the file into database
        string strQuery = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename;
        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = "image/jpeg";
        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
        InsertUpdateData(cmd);
    }

    protected void InsertPdf_Click(object sender, EventArgs e)
    {
        // Read the file and convert it to Byte Array
        string filePath = Server.MapPath("APP_DATA/TestPdf.pdf");
        string filename = Path.GetFileName(filePath);

        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        br.Close();
        fs.Close();

        //insert the file into database
        string strQuery = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename;
        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = "application/pdf";
        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
        InsertUpdateData(cmd);
    }

    protected void Retreive_Doc(object sender, EventArgs e)
    {
        string strQuery = "select Name, ContentType, Data from tblFiles where id=@id";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 1;
        DataTable dt = GetData(cmd);
        if (dt != null)
        {
            download(dt);
        }
    }
    protected void Retreive_Xls(object sender, EventArgs e)
    {
        string strQuery = "select Name, ContentType, Data from tblFiles where id=@id";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 2;
        DataTable dt = GetData(cmd);
        if (dt != null)
        {
            download(dt);
        }
    }
    protected void Retreive_Image(object sender, EventArgs e)
    {
        string strQuery = "select Name, ContentType, Data from tblFiles where id=@id";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 3;
        DataTable dt = GetData(cmd);
        if (dt != null)
        {
            download(dt); 
        }
    }

    protected void Retreive_Pdf(object sender, EventArgs e)
    {
        string strQuery = "select Name, ContentType, Data from tblFiles where id=@id";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 4;
        DataTable dt = GetData(cmd);
        if (dt != null)
        {
            download(dt);
        }
    }

    private Boolean InsertUpdateData(SqlCommand cmd)
    {
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            return false;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }

    private DataTable GetData(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlDataAdapter sda = new SqlDataAdapter();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;
        }
        catch 
        {
            return null;
        }
        finally
        {
            con.Close();
            sda.Dispose();
            con.Dispose();
        }
    }

    private void download (DataTable dt)
    {
        Byte[] bytes = (Byte[])dt.Rows[0]["Data"];
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = dt.Rows[0]["ContentType"].ToString();
        Response.AddHeader("content-disposition", "attachment;filename="
        + dt.Rows[0]["Name"].ToString());
        Response.BinaryWrite(bytes);
        Response.Flush(); 
        Response.End();
    }
 
  
}
