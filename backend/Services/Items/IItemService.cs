using backend.DataContracts;
using backend.Helpers;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Items
{
    public interface IItemService
    {
        List<Item> GetAll();
        Message GetById(int id);
        Message Buy(ItemBuyDataContract itemBuyObject);

        Message CreateRegularItem(Item item);
        Message CreateMapObjectItemHelper(Item item);
        Message CreateElixirTypeHealth(Item item);
        Message CreateElixirTypeEnergy(Item item);
    }
}
