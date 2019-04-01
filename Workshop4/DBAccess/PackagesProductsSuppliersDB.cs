﻿using ClassEntites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Author: Hayley Mead
 * last updated: 2019-03-31
 * WorkShop 4
 * Purpose: This code manages packages products suppliers 
 * Connects to Travel Expert Data Base to update, delete and add
 * information in the packages products suppliers  table
 */
namespace DBAccess
{
    public class PackagesProductsSuppliersDB
    {
        public static List<PackagesProductsSuppliers> GetPackagesProductsSuppliers()
        {
            List<PackagesProductsSuppliers> packagesProductsSuppliers = new List<PackagesProductsSuppliers>();
            SqlConnection connection = TravelExpertsDB.GetConnection(); //Connecting to TRavel Experts Database

            string select = "Select * FROM Packages_Products_Suppliers "; //selecting PackageId and Product Supplier ID

            SqlCommand sqlCommand = new SqlCommand(select, connection);
            try 
	        {	        
		        connection.Open();//opening connection 

                SqlDataReader read = sqlCommand.ExecuteReader();

                while (read.Read())
                {
                    PackagesProductsSuppliers packProdSupp = new PackagesProductsSuppliers();
                    packProdSupp.PackageId = (int)read["PackageId"];
                    packProdSupp.ProductSupplierId = (int)read["ProductSupplierId"];

                    packagesProductsSuppliers.Add(packProdSupp);
                }
	        }
	        catch (Exception ex)//catching all exeptions
	        {

		        throw ex;
	        }
            finally
            {
                connection.Close();//closing connection
            }
            return packagesProductsSuppliers; //returning List
        }//List of PPS end

        //Updating DataBase

        public static bool UpdatePackagesProductsSuppliers(PackagesProductsSuppliers oldPPS, PackagesProductsSuppliers newPPS)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();//connection to DB

            bool success = true;

            //finding record it needs to update "old" and replacing it with the "new" PPS info
            string update = "UPDATE PackagesProductsSupplierss SET " +
                            "ProductSupplierId = @NewProductSupplierId, " +
                            "WHERE PackageId = @OldPackageId " + 
                            "AND ProductSupplierId = @OldProductSupplierId";

            SqlCommand updateCmd = new SqlCommand(update, connection);

            updateCmd.Parameters.AddWithValue("@NewProductSupplierId", newPPS.ProductSupplierId);
            updateCmd.Parameters.AddWithValue("@OldPackageId", oldPPS.PackageId);
            updateCmd.Parameters.AddWithValue("@OldProductSupplierId", oldPPS.ProductSupplierId);

            try 
            {	        
		        connection.Open();
                int rowsUpdated = updateCmd.ExecuteNonQuery();
                if(rowsUpdated == 0) success = false; //if rows where not updated and success returns false 
            }
            catch (Exception ex)//catching all exeptions
            {
                throw ex;
            }
            finally
            {
                connection.Close();//closing connection
            }
            return success; //returning updated Info if it was "true"
        }//updating Method end

        public static bool DeletePackagesProductsSuppliers(PackagesProductsSuppliers packProdSupp)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();//connection to DB

            bool success = true;

            string delete = "DELETE FROM PackagesProductsSuppliers " +
                            "WHERE PackageId = @PackageId " +
                            "AND ProductSupplierId = @ProductSupplierId";

            SqlCommand deleteStatment = new SqlCommand(delete, connection);

            deleteStatment.Parameters.AddWithValue("@PackageId", packProdSupp.PackageId);
            deleteStatment.Parameters.AddWithValue("@ProductSupplierId", packProdSupp.ProductSupplierId);

            try
            {
                connection.Open();//opening connection
                int del = deleteStatment.ExecuteNonQuery();
                if (del == 0)
                    success = false; //zero rows have been deleted then we return false
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally 
            {
                connection.Close();
            }
            return success;
        }//delete method end
    }//Class
}//Namespace
