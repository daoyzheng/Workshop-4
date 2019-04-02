﻿using ClassEntites;
using DBAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Workshop4 {
    public partial class frmPackage : Form {
        // FORM-LEVEL VARIABLE
        // Get a list of all Package objects from database
        List<Package> packages = PackageDB.GetAllPackages();
        // Get a list of all Product_Suppliers objects from database
        List<ProductsSupplier> productsSuppliers = ProductsSupplierDB.GetAllProductsSuppliers();
        // Get a list of all Products objects from database
        List<Product> products = ProductDB.GetProducts();
        // Get a list of all Suppliers objects from database
        List<Supplier> suppliers = SupplierDB.GetSuppliers();
        // Get a list of all Package_Product_Suppliers by package id
        List<PackagesProductsSuppliers> pkgProdSupps = 
            PackagesProductsSuppliersDB.GetPackagesProductsSuppliers();

        public frmPackage() {
            InitializeComponent();
        }

        // Join related tables together and create view model
        private List<DummyPackage> JoinTables() {
            // Join Package_Product_Suppliers table with Product_Suppliers table and 
            // Products table and Suppliers table
            var pkgTable = from pkgProdSupp in pkgProdSupps
                           join pkgs in packages
                           on pkgProdSupp.PackageId equals pkgs.PackageId
                           join prodSupp in productsSuppliers
                           on pkgProdSupp.ProductSupplierId equals prodSupp.ProductSupplierId
                           join prod in products
                           on prodSupp.ProductId equals prod.ProductId
                           join supp in suppliers
                           on prodSupp.SupplierId equals supp.SupplierId
                           select new DummyPackage {
                               PackageId = pkgs.PackageId,
                               PkgName = pkgs.PkgName,
                               PkgBasePrice = pkgs.PkgBasePrice,
                               PkgStartDate = pkgs.PkgStartDate,
                               PkgEndDate =  pkgs.PkgEndDate,
                               PkgDesc = pkgs.PkgDesc,
                               PkgAgencyCommission = pkgs.PkgAgencyCommission,
                               ProductSupplierId =  prodSupp.ProductSupplierId,
                               ProdName = prod.ProdName,
                               SuppName = supp.SupName
                           };

            var pkgViewModel = pkgTable.ToList();

            return pkgViewModel;
        }

        private void frmPackage_Load(object sender, EventArgs e) {
            // Display a list of Package ids to the package Id combobox
            pkgNameComboBox.DisplayMember = "PkgName";
            pkgNameComboBox.ValueMember = "PackageId";
            pkgNameComboBox.DataSource = packages;
        }

        private void pkgNameComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                LoadPkgs();
            } catch(Exception ex) {
                MessageBox.Show("Error: " + ex.Message, ex.GetType().ToString());
            }
        }
        
        private void prodNameComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                // If option (None) is chosen
                if (prodNameComboBox.SelectedIndex == 0) {
                    LoadPkgs();
                    supNameComboBox.Text = string.Empty;
                    supNameComboBox.Enabled = false;
                } else {
                    supNameComboBox.Enabled = true;
                    LoadPkgProds();
                }
            } catch(Exception ex) {
                MessageBox.Show("Error: " + ex.Message, ex.GetType().ToString());
            }
        }
        private void supNameComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (supNameComboBox.SelectedIndex == 0) {
                    LoadPkgProds();
                } else {
                    LoadPkgProdSupps();
                }
            } catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, ex.GetType().ToString());
            }
        }

        // Load Packages
        private void LoadPkgs() {
            List<DummyPackage> dummyPackages = JoinTables();
            // Get the selected PackageID
            int pkgId = (int)pkgNameComboBox.SelectedValue;

            // Display View Model associated with the selected packageID
            var pkgList = dummyPackages.Where(p => p.PackageId == pkgId).ToList();

            // Bind to datagridview
            dummyPackageDataGridView.DataSource = pkgList;

            // Display available options of products for the selected packageID
            var prods = pkgList.Select(p => p.ProdName).ToList();

            // Add a (None) option to the list
            prods.Insert(0, "(None)");

            // Bind to Products Combo box
            prodNameComboBox.DataSource = prods;
        }

        // Load packages and products
        private void LoadPkgProds() {
            List<DummyPackage> dummyPackages = JoinTables();

            // Get currently selected pkg Id
            int pkgId = (int)pkgNameComboBox.SelectedValue;

            // Get currently selected products Name
            string prodName = prodNameComboBox.Text;

            var pkgList = dummyPackages.Where(p => p.PackageId == pkgId && 
                (string.Compare(p.ProdName, prodName) == 0)).ToList();

            // Bind to datagridview
            dummyPackageDataGridView.DataSource = pkgList;

            // Display available options of suppliers for the Selected PackageId and ProductId
            var supps = pkgList.Select(p => p.SuppName).ToList();

            // Add a (None) option to the list
            supps.Insert(0, "(None)");

            // Bind to Supplier combo box
            supNameComboBox.DataSource = supps;
        }

        private void LoadPkgProdSupps() {
            List<DummyPackage> dummyPackages = JoinTables();

            // Get currently selected pkg Id
            int pkgId = (int)pkgNameComboBox.SelectedValue;

            // Get currently selected products Name
            string prodName = prodNameComboBox.Text;

            // Get currently selected suppliers Name
            string suppName = supNameComboBox.Text;

            var pkgList = dummyPackages.Where(p => p.PackageId == pkgId && 
                (string.Compare(p.ProdName, prodName) == 0) &&
                (string.Compare(p.SuppName, suppName) == 0)).ToList();

            // Bind to datagridview
            dummyPackageDataGridView.DataSource = pkgList;
        }

        private void dummyPackageDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            // If row is empty, don't select, if row is not empty, select the product supplier id
            if (dummyPackageDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) {
                // When a single cell is seleted, it will select the whole row
                dummyPackageDataGridView.CurrentRow.Selected = true;
                int psId = (int)dummyPackageDataGridView.Rows[e.RowIndex].Cells[0].Value;
            }
        }

        private void btnAddPkg_Click(object sender, EventArgs e) {
            frmAddPackages frmAddPackages = new frmAddPackages();
            frmAddPackages.products = products;
            frmAddPackages.suppliers = suppliers;
            frmAddPackages.packages = packages;
            DialogResult result = frmAddPackages.ShowDialog();
            if (result == DialogResult.OK) {
                
            } else if (result == DialogResult.Retry) {
                
            }
        }
    }
}
