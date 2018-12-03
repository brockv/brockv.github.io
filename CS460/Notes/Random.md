### **Razor Examples**

#### **Html.ActionLink**

```c#
@Html.ActionLink("Edit", "Edit", new { id = item.ID }, new { @class = "actionLinkEdit", @data_toggle = "modal", @data_target = "#createModal", @data_id = item.ID }) |  
@Html.ActionLink("Bid", "Create", "Bids", new { id = item.ID }, new { @class = "actionLinkBid", @data_toggle = "modal", @data_target = "#createModal", @data_id = item.ID }) |  
@Html.ActionLink("Delete", "ConfirmDelete", new { id = item.ID }, new { @class = "actionLinkDelete btn-sm btn-danger", @data_toggle = "modal", @data_target = "#createModal", @data_id = item.ID })
```

#### **RedirectToAction With Parameter**

##### **Method One**
```c#
return RedirectToAction("Action", (optional) "Controller", new { id = 99 });
```

##### **Method Two**
```c#
return RedirectToAction( "Main", new RouteValueDictionary( 
    new { controller = controllerName, action = "Main", Id = Id } ) );
```