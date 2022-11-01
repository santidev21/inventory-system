using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using InventoryApp.Models;

namespace InventoryApp.Logic
{
    public class Items
    {
        public DataTable loadItems(string cText)
        {
            SQLiteDataReader Response;
            DataTable Table = new DataTable();
            SQLiteConnection SqlCon = new SQLiteConnection();
            try
            {
                SqlCon = Connection.getInstance().createConection();
                cText = "%" + cText.Trim() + "%";
                string SQLCommand = "select id,name,quantity,brand,category, addedById from itemsTb where name like '" + cText + "'";
                SQLiteCommand Comando = new SQLiteCommand(SQLCommand, SqlCon);
                SqlCon.Open();
                Response = Comando.ExecuteReader();
                Table.Load(Response);
                return Table;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }
        public string saveItem(int nOption, ItemsModel oAr)
        {
            string Answ = "";
            SQLiteConnection SqlCon = new SQLiteConnection();
            try
            {
                SqlCon = Connection.getInstance().createConection();
                string Sql_command = "";
                if (nOption ==1)
                {
                    Sql_command = "insert into itemsTb(name,quantity,brand,category,addedById) "+
                        "values('"+oAr.itemName+"','" + oAr.quantityItem + "','" + oAr.brandItem + "','" + oAr.categoryItem+ "','" + oAr.createdByItem + "')";
                } else
                {
                    Sql_command = "update itemsTb set name='"+
                        oAr.itemName+"', quantity='"+
                        oAr.quantityItem+"', brand='"+
                        oAr.brandItem+"', category='"+
                        oAr.categoryItem+"' "+
                        "where id='"+oAr.itemId+"'"; 
                }
                
                SQLiteCommand Command = new SQLiteCommand(Sql_command, SqlCon);
                SqlCon.Open();
                Answ = Command.ExecuteNonQuery() >= 1 ? "OK" : "Error saving item";
                return Answ;
            }
            catch (Exception ex)
            {

                return Answ = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }
        public string deleteItem(int nIdtoDelete)
        {
            string Answ = "";
            SQLiteConnection SqlCon = new SQLiteConnection();
            try
            {
                SqlCon = Connection.getInstance().createConection();
                string Sql_command = "delete from itemsTb where id='"+nIdtoDelete+"'";

                SQLiteCommand Command = new SQLiteCommand(Sql_command, SqlCon);
                SqlCon.Open();
                Answ = Command.ExecuteNonQuery() >= 1 ? "OK" : "Error deleting the item.";
                return Answ;
            }
            catch (Exception ex)
            {

                return Answ = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }
        public DataTable loadCategories()
        {
            SQLiteDataReader Response;
            DataTable Table = new DataTable();
            SQLiteConnection SqlCon = new SQLiteConnection();
            try
            {
                SqlCon = Connection.getInstance().createConection();
                string SQLCommand = "select category, categoryId  from categoriesTb";
                SQLiteCommand Comando = new SQLiteCommand(SQLCommand, SqlCon);
                SqlCon.Open();
                Response = Comando.ExecuteReader();
                Table.Load(Response);
                return Table;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

    }
}
