using System;
using System.Collections.Generic;
using Xunit;
using System.Data;
using System.Data.SqlClient;


namespace Inventory
{
  public class InventoryTest
  {
    public InventoryTest()
    {
      DBConfiguration.ConnectionString =
      "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=inventory_test;Integrated Security=SSPI;";
    }
    // public void Dispose()
    // {
    //   Task.DeleteAll();
    // }
    [Fact]
    public void T1_DatabaseMtyAtFirst()
    {
      //Arange, Act
      int result = Inventory.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void T2_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Inventory firstItem = new Inventory("Of Mice and Men");
      Inventory secondItem = new Inventory ("Of Mice and Men");
      //Assert
      Assert.Equal(firstItem, secondItem);
    }
    [Fact]
    public void T3_Save_SavesToDatabase()
    {
      //Arrange
      Inventory testInventory = new Inventory("Of Mice and Men");
      //Action
      testInventory.Save();
      List<Inventory> result = Inventory.GetAll();
      List<Inventory> testList = new List<Inventory>{testInventory};
      //Assert
      Assert.Equal(testList, result);
    }
  }
}
