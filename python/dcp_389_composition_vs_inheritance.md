# Question

This problem was asked by Google.

Explain the difference between composition and inheritance. In which cases would you use each?

# Answer

Both composition and inheritance are widely used techniques in the Object-Oriented Programming.
Both are used in order to make more complex things on top of simpler ones.
Both also are heavily object centric and will require you to create a new *class*.

Compositions suggest you to create a class that will hold reference to other classes inside and will expose an interface
completely decoupled from the objects that it uses internally. You are not bound by any limits like amounts of classes
you can use internally and, again, interface you are server can also be arbitrary. It's frequently told that composition
corresponds to "has a" relationship when mapped to the real world.

Inheritance suggest to create derived class where everything that the base class could do your new class also should be
able to do. Inherited class can do *something else* -- it's there to extend the functionality provided by the base
class. However, we are much more restricted -- it should be *at least as capable* as the base class is. Additionally,
quite a bit of languages will not let you inherit implementations from more than one class as it leads to tricky edge
cases like famous "diamond problem" where it might become really tricky to pick correct method implementation if it's
present in multiple base classes. Inheritance is usually though of as "is a" relationship. However, one should be very
carefully as things might not map well from the conventional understanding. For instance, square "is a" rectangle, 
however, if you will try to implement *mutable* rectangle and then create square as derived class you will most likely
get your in the trouble. Derived class should *not* shrink the contract provided by the base class. Everywhere where
base class is good enough *any* derived class should work out as well (that's basically an essence of the well-known
Barbara's Liskov Substitution Principle).

Summarizing, is a lot of cases both techniques could probably be used and understanding of different properties of both
should drive the choice. However, in a first approximation if an object you want to create really IS an extension of
existing class and it only *extends* the behaviour, than choose the *inheritance*. If however, if find that you just
need some functionality implemented by one or several other classes, then choose *composition*.

**PS**. Sure, it is much more nuanced than that...