from algopy import ARC4Contract, arc4, UInt64


class HelloWorld(ARC4Contract):
    @arc4.abimethod()
    def hello(self, name: arc4.String) -> arc4.String:
        return "Hello, " + name
    
    @arc4.abimethod()
    def addition(self, a:UInt64, b:UInt64) -> UInt64:
        total = a+b
        return total
