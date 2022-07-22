// See https://aka.ms/new-console-template for more information
using Intersect;


var accountPermissions = new List<AccountPermissions>();
accountPermissions.Add( new AccountPermissions{Id = 1, AccountId = 1});
accountPermissions.Add( new AccountPermissions{Id = 2, AccountId = 2});
accountPermissions.Add( new AccountPermissions{Id = 3, AccountId = 3});
accountPermissions.Add( new AccountPermissions{Id = 4, AccountId = 4});
accountPermissions.Add( new AccountPermissions{Id = 5, AccountId = 5});
accountPermissions.Add( new AccountPermissions{Id = 6, AccountId = 6});

var accountProduct = new List<AccountProduct>();
accountProduct.Add(new AccountProduct{ProductId=1, AccountPermissionId=1});
accountProduct.Add(new AccountProduct{ProductId=2, AccountPermissionId=1});
accountProduct.Add(new AccountProduct{ProductId=3, AccountPermissionId=1});

accountProduct.Add(new AccountProduct{ProductId=1, AccountPermissionId=2});
accountProduct.Add(new AccountProduct{ProductId=2, AccountPermissionId=2});
accountProduct.Add(new AccountProduct{ProductId=4, AccountPermissionId=2});

accountProduct.Add(new AccountProduct{ProductId=1, AccountPermissionId=6});
accountProduct.Add(new AccountProduct{ProductId=2, AccountPermissionId=6});
accountProduct.Add(new AccountProduct{ProductId=3, AccountPermissionId=6});

var accountList = accountProduct.AsQueryable().Where(p => p.ProductId ==1).
Join(accountPermissions, a => a.AccountPermissionId, p=>p.Id, (a,p) => p.AccountId)
.Intersect(accountProduct.AsQueryable().Where(p => p.ProductId ==2).
Join(accountPermissions, a => a.AccountPermissionId, p=>p.Id, (a,p) => p.AccountId)).ToList();;


var productList = new List<int>{1,2,4};
var queryableList = new List<IQueryable<int>>();
foreach(int i in productList){
    queryableList.Add(accountProduct.AsQueryable().Where(p => p.ProductId == i)
    .Join(accountPermissions, a => a.AccountPermissionId, p=>p.Id, (a,p) => p.AccountId));
}

var leftQuery = queryableList[0];
foreach(var rightQuery in queryableList)
{
    if (rightQuery != leftQuery)
    {
        leftQuery = leftQuery.Intersect(rightQuery);
    }
}

accountList = leftQuery.ToList();

Console.WriteLine("here we go!");
