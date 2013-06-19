function checkForNumeric(obj) {
	if( /[^0-9\.]|-{2,}/gi.test(obj.value) ) {
		alert("Must enter a positive numeric value");
		obj.focus();
		obj.select();
		return false;
	}
	return true;
}

function ManipulateGrid(gvid)
        {
        
            var gv1 = document.getElementById("GridView1");
            var gv2 = document.getElementById("GridView3");
            //alert(gv.rows.length);
            var OverallTotal = 0;
            var QuantityTotal = 0;
            var ServiceFeeTotal = 0;
            var ServiceFeeRow = 0;
            var SFP = document.getElementById("hdSFP");
            var SFC = document.getElementById("hdSFC");
            var SFM = document.getElementById("hdSFM");
            
            var gv = gv1;
            var gvother = gv2;
            if (gvid == 1){
                gv = gv2;
                gvother = gv1;
            }
            for (i=1; i<gv.rows.length; i++)
            { 
                //var cell = gv.rows[i].cells;
                //var HTML = cell[0].innerHTML;
                //var Price = HTML.indexof("lblPrice");
             var extradigit = "0";
             if (i > 9)
             {
                extradigit = "";
             }
             
             var Price1 = document.getElementById("GridView1_ctl" + extradigit + i + "_lblPrice");
             var Quantity1 = document.getElementById("GridView1_ctl" + extradigit + i + "_ddlQuantity");
             var Total1 = document.getElementById("GridView1_ctl" + extradigit + i + "_lblTotal");
             var Donatetxt = document.getElementById("GridView1_ctl" + extradigit + i + '_txtDonate');
             
             var Price2 = document.getElementById("GridView3_ctl" + extradigit + i + "_lblPrice");
             var Quantity2 = document.getElementById("GridView3_ctl" + extradigit + i + "_ddlQuantity");
             var Total2 = document.getElementById("GridView3_ctl" + extradigit + i + "_lblTotal");                       
             
            
             var Price = Price1;
             var Quantity = Quantity1;
             var Total = Total1;             
             
             
              if (Price != null && Quantity != null)
              {                            
              var RowTotal = (Price.innerHTML.replace("$", "") * Quantity.selectedIndex);
              Total.innerHTML = "$ " + RowTotal.toFixed(2);
              
              OverallTotal = OverallTotal + RowTotal;
              QuantityTotal = QuantityTotal + Quantity.selectedIndex;
              ServiceFeeRow = 0;
              if (RowTotal != 0)
                {                
                ServiceFeeRow = (RowTotal * parseFloat(SFP.value)) + parseFloat(SFC.value * Quantity.selectedIndex);                                                   
                if (parseFloat(ServiceFeeRow) > parseFloat(SFM.value * Quantity.selectedIndex))
                    {
                        ServiceFeeRow = parseFloat(SFM.value* Quantity.selectedIndex);
                        
                    }                    
                ServiceFeeTotal += ServiceFeeRow;
                }                                                        
              //var total = parseFloat();
              //alert(Price.innerHTML.replace("$", "") * Quantity.selectedIndex);                      
              }
              else if (Donatetxt != null)
              {
                if (checkForNumeric(Donatetxt))
                {
                    var RowTotal = parseFloat(Donatetxt.value);
                    
                    Total.innerHTML = "$ " + RowTotal.toFixed(2);
                    OverallTotal = OverallTotal + RowTotal;
                    var ServiceFeeRow = 0;
                    //Service fee row
                    if (RowTotal != 0)
                    {                
                    ServiceFeeRow = (RowTotal * parseFloat(SFP.value)) + parseFloat(SFC.value * 1);
                    if (parseFloat(ServiceFeeRow) > parseFloat(SFM.value * 1))
                        {
                            ServiceFeeRow = parseFloat(SFM.value* 1);
                            
                        }                    
                    ServiceFeeTotal += ServiceFeeRow;
                    }                                                                            
                 }//if its numeric
              }//donatetxt != null
              else if(i == gv.rows.length-1){
                /*var ServiceFeeTotal = 0;
                if (OverallTotal != 0)
                {                    
                    ServiceFeeTotal = (OverallTotal * parseFloat(SFP.value)) + parseFloat(SFC.value);   
                    if ((ServiceFeeTotal* QuantityTotal) > parseFloat(SFM.value))
                    {
                        ServiceFeeTotal = parseFloat(SFM.value* QuantityTotal);
                    }
                } */                     
                Total.innerHTML = "$ " + ServiceFeeTotal.toFixed(2);

                OverallTotal = OverallTotal + ServiceFeeTotal;
              }
            }            
            if (i > 9)
             {
                extradigit = "";
             }
            var OverallTotalText = document.getElementById("GridView1_ctl" + extradigit + i + "_lblTotalOverall");
           
            OverallTotalText.innerHTML = "$ " + OverallTotal.toFixed(2);
           
            document.getElementById("hdServiceFee").value = ServiceFeeTotal;
            document.getElementById("hdOverallTotal").value = OverallTotal.toFixed(2);
            
            var calendartype = document.getElementById("hdCalendarType");
            if (calendartype != null)
            {
                if (calendartype.value == "1") //kiting lessons
                {
                    extradigit = "0";
                    
                        for (i=2; i<gv.rows.length; i++)
                        {
                        if (i > 9)
                             {
                                extradigit = "";
                             }                             
                            var Quantity1 = document.getElementById("GridView1_ctl" + extradigit + i + "_ddlQuantity"); 
                            if (Quantity1 != null)
                            {
                            if (OverallTotal > 0) //disable all other dropdowns, to prevent from booking more than 1 lesson
                            {
                                if (Quantity1.selectedIndex == 0)
                                {
                                    Quantity1.disabled = true;
                                }
                            }
                            else if (OverallTotal == 0) //enable all other dropdowns, to prevent from booking more than 1 lesson
                            {
                                Quantity1.disabled = false;
                            }
                                
                            }                           
                        }                                                            
                }
            }
        }

