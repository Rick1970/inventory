using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Inventory
{
  public class Inventory
  {
    private string _item;
    private int _id;

    public Inventory(string item, int id = 0)
    {
      _item = item;
      _id = id;
    }
    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Inventory))
      {
        return false;
      }
      else
      {
        Inventory newInventory = (Inventory) otherItem;
        bool descriptionEquality = (this.GetItem() == newInventory.GetItem());
        return (descriptionEquality);
      }

    }
    public override int GetHashCode()
    {
      return this.GetItem().GetHashCode();
    }
    public string GetItem()
    {
      return _item;
    }
    public void SetItem(string newItem)
    {
      _item = newItem;
    }
    public int GetId()
    {
      return _id;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO items (item) OUTPUT INSERTED.id VALUES (@inventoryItem);", conn);
      SqlParameter itemParameter = new SqlParameter();
      itemParameter.ParameterName = "@inventoryItem";
      itemParameter.Value = this.GetItem();
      cmd.Parameters.Add(itemParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static List<Inventory> GetAll()
    {
      List<Inventory> allInventory = new List<Inventory>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM items;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int inventoryId = rdr.GetInt32(0);
        string inventoryDescription = rdr.GetString(1);
        Inventory newInventory = new Inventory(inventoryDescription, inventoryId);
        allInventory.Add(newInventory);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allInventory;
    }
  }
}
