//Basic
let age: number = 23;
if (age < 50) age += 10;
console.log("AGE:" + age);

//Any
let sales = 123_456_789;
let course = "TypeScript";
let is_published = true;
let level;

function render(document: any) {
  console.log(document);
}

//Array
let numbers: number[] = [];
numbers.forEach((n) => n.toString());

//Tuples
let user: [number, string] = [1, "Drashti"];
user.push(1);
console.log("User:" + user[1].toString());

//Enums
// const small = 1;
// const medium =2;
// const large =3;

//PascalCase
//const enum Size {Small = 1, Medium ,Large};
enum Size {
  Small = 1,
  Medium,
  Large,
}
let mySize: Size = Size.Large;
console.log("Size:"+mySize);

//Functions
function calculateTax(income: number, taxYear = 2022): number {
  if (taxYear < 2022) return income * 1.2;
  return income * 1.3;
}
console.log("Income:" + calculateTax(10_000, 2023));
calculateTax(10_000, 2023);

//Objects
let employee: {
  readonly id: number;
  name: string;
  retire: (date: Date) => void;
} = { id: 1, name: "drashti" ,retire:(date:Date)=> {
    console.log("Date:"+date)
}};
console.log("Employee:" + employee.name);

//Type Aliases
type Employee = {
  readonly id: number;
  name: string;
  retire: (date: Date) => void;
}

//Union Types
function kgToLbs(weight:number|string):number{
    //Narrowing
    if (typeof weight === "number")
        return weight * 2.2;
    else
        return parseInt(weight)*2.2;
}
kgToLbs(10);
kgToLbs('10kg');
console.log("Weight:"+kgToLbs('10kg'))

//Intersection Types
//example : let weight:number & string;

type Dragggable ={
    drag:()=>void
};

type Resizable = {
    resize:()=>void
};

type UIWidget = Dragggable & Resizable;

let textbox: UIWidget = {
    drag:()=>{},
    resize:()=>{}
}

//Literal Types(exact or specific value)
type Quantity = 50 | 100;
let quantity: 50 | 100 = 100;
type Metric = 'cm' | 'inch';

//Nullable Types
function greet(name: string|null|undefined){
    if(name)
        console.log(name.toLocaleUpperCase());
    else
        console.log('Hola!');
}
greet(undefined);

//Optional Chaining

type Customer = {
    birthday: Date
}
function getCustomer(id:number): Customer|null|undefined{
    return id === 0?null : {birthday: new Date()}
}

let customer = getCustomer(1);
//Optional property access operator
//if (customer!==null && customer!==undefined)
    console.log(customer?.birthday?.getFullYear());

//optional element access operator
//customers?.[0]

//optional call
//let log: any = (message: string) => console.log(message);
let log: any = null;
log?.('a');