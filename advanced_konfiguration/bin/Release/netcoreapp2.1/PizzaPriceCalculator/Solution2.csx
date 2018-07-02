using System;
var total = 0;
if (PizzasOrdered > 10)
{
    total = Convert.ToInt32(PizzasOrdered * PricePerPizza * 0.8);
}
else
{
    total = PizzasOrdered * PricePerPizza;
}
total