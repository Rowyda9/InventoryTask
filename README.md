# InventoryTask
Please find login username & password in seed.cs file.

We have an inventory with :
● users(name, email, phone, role)
● products(name, barcode, description, weight, status(sold, inStock, damaged))
● categories (name)
● status (name)


Using DDD, SQL server, EF Core for the database provider, .Net core, no UI needed just
the APIs
● Create 3 APIs
1) Count the number of products sold, damaged and inStock
2) Change the status of a product
3) Sell a product
-----------------------------------------

● Other  APIs
1) Product ( GetAll, Create, Update, SoftDelete, Delete )
2) Get products by categroy or status in ( GetAllByPagination API )
3) Category ( GetAll, Create, Update, SoftDelete, Delete )
4) Status ( GetAll, Create, Update, SoftDelete, Delete )


