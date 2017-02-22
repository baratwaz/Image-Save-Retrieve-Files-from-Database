Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Partial Class VB
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable
        Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("conString").ConnectionString
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Response.Write(ex.Message)
            Return Nothing
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    Public Function InsertUpdateData(ByVal cmd As SqlCommand) As Boolean
        Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("conString").ConnectionString
        Dim con As New SqlConnection(strConnString)
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Response.Write(ex.Message)
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Protected Sub InsertDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Read the file and convert it to Byte Array
        Dim filePath As String = Server.MapPath("APP_DATA/TestDoc.docx")
        Dim filename As String = Path.GetFileName(filePath)

        Dim fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))
        br.Close()
        fs.Close()

        'insert the file into database
        Dim strQuery As String = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)"
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename
        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = "application/vnd.ms-word"
        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes
        InsertUpdateData(cmd)
    End Sub

    Protected Sub InsertXls_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Read the file and convert it to Byte Array
        Dim filePath As String = Server.MapPath("APP_DATA/Testxls.xlsx")
        Dim filename As String = Path.GetFileName(filePath)

        Dim fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))
        br.Close()
        fs.Close()

        'insert the file into database
        Dim strQuery As String = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)"
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename
        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = "application/vnd.ms-excel"
        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes
        InsertUpdateData(cmd)
    End Sub

    Protected Sub InsertImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Read the file and convert it to Byte Array
        Dim filePath As String = Server.MapPath("APP_DATA/TestImage.jpg")
        Dim filename As String = Path.GetFileName(filePath)

        Dim fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))
        br.Close()
        fs.Close()

        'insert the file into database
        Dim strQuery As String = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)"
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename
        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = "image/jpeg"
        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes
        InsertUpdateData(cmd)
    End Sub
    Protected Sub InsertPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        ' Read the file and convert it to Byte Array
        Dim filePath As String = Server.MapPath("APP_DATA/TestPdf.pdf")
        Dim filename As String = Path.GetFileName(filePath)

        Dim fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))
        br.Close()
        fs.Close()

        'insert the file into database
        Dim strQuery As String = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)"
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename
        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = "application/pdf"
        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes
        InsertUpdateData(cmd)
    End Sub

    Protected Sub Retreive_Doc(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Dim strQuery As String = "select Name, ContentType, Data from tblFiles where id=@id"
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 1
        Dim dt As DataTable = GetData(cmd)
        If dt IsNot Nothing Then
            download(dt)
        End If
    End Sub

    Protected Sub Retreive_Xls(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Dim strQuery As String = "select Name, ContentType, Data from tblFiles where id=@id"
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 2
        Dim dt As DataTable = GetData(cmd)
        If dt IsNot Nothing Then
            download(dt)
        End If
    End Sub

    Protected Sub Retreive_Image(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click
        Dim strQuery As String = "select Name, ContentType, Data from tblFiles where id=@id"
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 3
        Dim dt As DataTable = GetData(cmd)
        If dt IsNot Nothing Then
            download(dt)
        End If
    End Sub

    Protected Sub Retreive_Pdf(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton4.Click
        Dim strQuery As String = "select Name, ContentType, Data from tblFiles where id=@id"
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 4
        Dim dt As DataTable = GetData(cmd)
        If dt IsNot Nothing Then
            download(dt)
        End If
    End Sub

    Protected Sub download(ByVal dt As DataTable)
        Dim bytes() As Byte = CType(dt.Rows(0)("Data"), Byte())
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = dt.Rows(0)("ContentType").ToString()
        Response.AddHeader("content-disposition", "attachment;filename=" & dt.Rows(0)("Name").ToString())
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub

   
   
End Class
