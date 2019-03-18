﻿using ClassEntites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess {
    public static class ProductsSupplierDB {

        // Get A single products supplier from the DB
        public static ProductsSupplier GetProductsSupplierByProductSupplierId(int productSupplierId) {
            ProductsSupplier prodSuppObj = null;
            string selectStatement = "SELECT ProductSupplierId, ProductId, SupplierId " +
                                     "FROM Products_Suppliers " +
                                     "WHERE ProductSupplierId = @productSupplierId";

            // Get connection to Travel Experts DB
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // Create a select command object
            SqlCommand selectCmd = new SqlCommand(selectStatement, connection);

            // Assign value to parameter(s)
            selectCmd.Parameters.AddWithValue("@productSupplierId", productSupplierId);

            // Execute the select command and start the reading process from DB
            try {
                connection.Open();
                SqlDataReader dr = selectCmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read()) { // if customer exists
                    prodSuppObj = new ProductsSupplier();
                    prodSuppObj.ProductSupplierId = (int)dr["ProductSupplierId"];

                    // Both Product ID and Supplier ID can be null, need to verify while reading
                    // from DB
                    if (dr["ProductId"] is DBNull) {
                        prodSuppObj.ProductId = null; 
                    } else {
                        prodSuppObj.ProductId = (int)dr["ProductId"];
                    }
                    if (dr["SupplierId"] is DBNull) {
                        prodSuppObj.SupplierId = null;
                    } else {
                        prodSuppObj.SupplierId = (int)dr["SupplierId"];
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                connection.Close();
            }

            return prodSuppObj;
        }
        
        // Get All products suppliers from the DB
        public static List<ProductsSupplier> GetAllProductsSuppliers() {
            List<ProductsSupplier> prodSuppList = new List<ProductsSupplier>();
            string selectStatement = "SELECT ProductSupplierId, ProductId, SupplierId " +
                                     "FROM Products_Suppliers " +
                                     "ORDER BY ProductSupplierId";

            // Get connection to Travel Experts DB
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // Create a select command object
            SqlCommand selectCmd = new SqlCommand(selectStatement, connection);

            // Execute the select command and start the reading process from DB
            try {
                connection.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read()) {
                    ProductsSupplier prodSuppObj = new ProductsSupplier();
                    prodSuppObj.ProductSupplierId = (int)dr["ProductSupplierId"];

                    // Both Product ID and Supplier ID can be null, need to verify while reading
                    // from DB
                    if (dr["ProductId"] is DBNull) {
                        prodSuppObj.ProductId = null; 
                    } else {
                        prodSuppObj.ProductId = (int)dr["ProductId"];
                    }
                    if (dr["SupplierId"] is DBNull) {
                        prodSuppObj.SupplierId = null;
                    } else {
                        prodSuppObj.SupplierId = (int)dr["SupplierId"];
                    }

                    // Add to product suppliers list
                    prodSuppList.Add(prodSuppObj);
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                connection.Close();
            }

            return prodSuppList;
        }

        // Get Product Suppliers by product id
        public static List<ProductsSupplier> GetProductsSupplierByProductId(int productId) {
            List<ProductsSupplier> prodSuppList = new List<ProductsSupplier>();
            string selectStatement = "SELECT ProductSupplierId, ProductId, SupplierId " +
                                     "FROM Products_Suppliers " +
                                     "WHERE ProductId = @productId";

            // Get connection to Travel Experts DB
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // Create a select command object
            SqlCommand selectCmd = new SqlCommand(selectStatement, connection);

            // Assign value to parameter(s)
            selectCmd.Parameters.AddWithValue("@productId", productId);

            // Execute the select command and start the reading process from DB
            try {
                connection.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read()) {
                    ProductsSupplier prodSuppObj = new ProductsSupplier();
                    prodSuppObj.ProductSupplierId = (int)dr["ProductSupplierId"];

                    // Both Product ID and Supplier ID can be null, need to verify while reading
                    // from DB
                    if (dr["ProductId"] is DBNull) {
                        prodSuppObj.ProductId = null; 
                    } else {
                        prodSuppObj.ProductId = (int)dr["ProductId"];
                    }
                    if (dr["SupplierId"] is DBNull) {
                        prodSuppObj.SupplierId = null;
                    } else {
                        prodSuppObj.SupplierId = (int)dr["SupplierId"];
                    }
                    prodSuppList.Add(prodSuppObj);
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                connection.Close();
            }

            return prodSuppList;
        }

        // Get Product Suppliers by supplier id
        public static List<ProductsSupplier> GetProductsSupplierBySupplierId(int supplierId) {
            List<ProductsSupplier> prodSuppList = new List<ProductsSupplier>();
            string selectStatement = "SELECT ProductSupplierId, ProductId, SupplierId " +
                                     "FROM Products_Suppliers " +
                                     "WHERE SupplierId = @supplierId";

            // Get connection to Travel Experts DB
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // Create a select command object
            SqlCommand selectCmd = new SqlCommand(selectStatement, connection);

            // Assign value to parameter(s)
            selectCmd.Parameters.AddWithValue("@supplierId", supplierId);

            // Execute the select command and start the reading process from DB
            try {
                connection.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read()) { // if customer exists
                    ProductsSupplier prodSuppObj = new ProductsSupplier();
                    prodSuppObj.ProductSupplierId = (int)dr["ProductSupplierId"];

                    // Both Product ID and Supplier ID can be null, need to verify while reading
                    // from DB
                    if (dr["ProductId"] is DBNull) {
                        prodSuppObj.ProductId = null; 
                    } else {
                        prodSuppObj.ProductId = (int)dr["ProductId"];
                    }
                    if (dr["SupplierId"] is DBNull) {
                        prodSuppObj.SupplierId = null;
                    } else {
                        prodSuppObj.SupplierId = (int)dr["SupplierId"];
                    }
                    prodSuppList.Add(prodSuppObj);
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                connection.Close();
            }

            return prodSuppList;
        }

        // Insert a new product 
        public static int AddProductsSupplier(ProductsSupplier prodSuppObj) {
            int prodSuppId = 0;
            string insertStatement = "INSERT INTO Products_Suppliers (ProductId, SupplierId) " +
                                     "OUTPUT Inserted.ProductSupplierId " +
                                     "VALUES (@ProductId, @SupplierId)";
            
            // Get connection to Travel Experts DB
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // Create an insert command object
            SqlCommand insertCmd = new SqlCommand(insertStatement, connection);

            // Assign value to parameter(s)
            // Verify if Product ID from object is null
            if (prodSuppObj.ProductId == null) {
                insertCmd.Parameters.AddWithValue("@ProductId", DBNull.Value);
            } else {
                insertCmd.Parameters.AddWithValue("@ProductId", prodSuppObj.ProductId);
            }
            // Verify if Supplier ID from object is null
            if (prodSuppObj.SupplierId == null) {
                insertCmd.Parameters.AddWithValue("@SupplierId", DBNull.Value);
            } else {
                insertCmd.Parameters.AddWithValue("@SupplierId", prodSuppObj.SupplierId);
            }

            // Execute the insert command
            try {
                connection.Open();
                // Returns the auto generated ProductSupplierId
                prodSuppId = (int)insertCmd.ExecuteScalar();
            } catch (Exception ex) {
                throw ex;
            } finally {
                connection.Close();
            }

            return prodSuppId;
        }

        // Update Products Supplier Table
        public static bool UpdateProductsSupplier(ProductsSupplier oldProdSuppObj, ProductsSupplier newProdSuppObj) {
            bool updateSuccess = true;
            string updateStatement = "UPDATE Products_Suppliers " +
                                     "SET ProductId = @newProductId, " +
                                     "SupplierId = @newSupplierId " +
                                     "WHERE ProductSupplierId = @oldProductSupplierId " +
                                     "AND (ProductId = @oldProductId " +
                                     "OR ProductId IS NULL AND @oldProductId IS NULL) " +
                                     "AND (SupplierId = @oldSupplierId " +
                                     "OR SupplierId IS NULL AND @oldSupplierId IS NULL)";

            // Get connection to Travel Experts DB
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // Create a select command object
            SqlCommand updateCmd = new SqlCommand(updateStatement, connection);

            // Assign value to parameter(s)
            // Verify if newProdSuppObj.ProductId is null
            if (newProdSuppObj.ProductId == null) {
                updateCmd.Parameters.AddWithValue("@newProductId", DBNull.Value);
            } else { 
                updateCmd.Parameters.AddWithValue("@newProductId", newProdSuppObj.ProductId);
            }

            // Verify if newProdSuppObj.SupplierId is null
            if (newProdSuppObj.SupplierId == null) {
                updateCmd.Parameters.AddWithValue("@newSupplierId", DBNull.Value);
            } else {
                updateCmd.Parameters.AddWithValue("@newSupplierId", newProdSuppObj.SupplierId);
            }

            // Execute the update command
            try {
                connection.Open();
                int rowsUpdated = updateCmd.ExecuteNonQuery();
                // Check for concurrency, another user might have updated or deleted in the meantime
                if (rowsUpdated == 0) updateSuccess = false;
            } catch (Exception ex) {
                throw ex;
            } finally {
                connection.Close();
            }

            return updateSuccess;
        }
    }
}