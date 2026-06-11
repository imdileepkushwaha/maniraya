using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicTier
{
    public class Documet_Upload_class
    {

        public string Registration_no { get; set; }
        public string Doc_Name { get; set; }
        public string Doc_Path { get; set; }
        public string Updated_By { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get;set;}
        public string Heading { get; set; }
        public string Type { get; set; }
        public string PK_ContentId { get; set; }
        public string PK_DocumentId { get; set; }
        public DataSet Upload_Document(Documet_Upload_class ObjDuc)
        {
          

            SqlParameter[] para ={new SqlParameter ("@Doc_Path",ObjDuc.Doc_Path),
                                     new SqlParameter ("@Doc_Name",ObjDuc.Doc_Name),
                                     new SqlParameter ("@Description",ObjDuc.Description),
                                     
                                     };
            DataSet ds = DBHelper.ExecuteQuery("InsertImageWithTitle", para);
       
            return ds;
        }
        public DataSet Upload_Content(Documet_Upload_class ObjDuc)
        {


            SqlParameter[] para ={
                                     new SqlParameter ("@Doc_Name",ObjDuc.Doc_Name),
                                     new SqlParameter ("@Description",ObjDuc.Description),
                                      new SqlParameter ("@CreatedBy",ObjDuc.CreatedBy),
                                      new SqlParameter ("@Heading",ObjDuc.Heading),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("InsertContent", para);

            return ds;
        }
        public DataSet Update_Content(Documet_Upload_class ObjDuc)
        {

            SqlParameter[] para ={
                                     new SqlParameter ("@Doc_Name",ObjDuc.Doc_Name),
                                     new SqlParameter ("@Description",ObjDuc.Description),
                                      new SqlParameter ("@CreatedBy",ObjDuc.CreatedBy),
                                      new SqlParameter ("@Heading",ObjDuc.Heading),
                                      new SqlParameter ("@PK_ContentId",ObjDuc.PK_ContentId)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("UpdateContent", para);

            return ds;
        }
        public DataSet GetUpload_Document(Documet_Upload_class ObjDuc)
        {


            SqlParameter[] para ={
                                      new SqlParameter ("@CreatedBy",ObjDuc.CreatedBy),
                                      new SqlParameter ("@PK_DocumentId",ObjDuc.PK_DocumentId),
                                       new SqlParameter ("@Title",ObjDuc.Doc_Name),
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetProfileImage", para);

            return ds;
        }
        public DataSet GetDelete_Document(Documet_Upload_class ObjDuc)
        {


            SqlParameter[] para ={
                                      new SqlParameter ("@CreatedBy",ObjDuc.CreatedBy),
                                      new SqlParameter ("@PK_DocumentId",ObjDuc.PK_DocumentId),
                                       new SqlParameter ("@Title",ObjDuc.Doc_Name),
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetDeleteImage", para);

            return ds;
        }
        public DataSet DeleteUpload_Document(Documet_Upload_class ObjDuc)
        {


            SqlParameter[] para ={
                                      
                                      new SqlParameter ("@PK_DocumentId",ObjDuc.PK_DocumentId),
                                     };
            DataSet ds = DBHelper.ExecuteQuery("DeleteProfileImage", para);

            return ds;
        }
        public DataSet GetUpload_Content(Documet_Upload_class ObjDuc)
        {


            SqlParameter[] para ={
                                      new SqlParameter ("@PK_ContentId",ObjDuc.PK_ContentId),
                                      new SqlParameter ("@Type",ObjDuc.Type),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetContent", para);

            return ds;
        }
        public DataSet GetInvoice_Data(Documet_Upload_class ObjDuc)
        {


            SqlParameter[] para ={
                                      
                                      new SqlParameter ("@UserId",ObjDuc.Type),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetInvoice", para);

            return ds;
        }
        public DataSet GetInvoice_Status(Documet_Upload_class ObjDuc)
        {


            SqlParameter[] para ={
                                      
                                      new SqlParameter ("@UserId",ObjDuc.Type),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetInvoiceStatus", para);

            return ds;
        }
    }
}
