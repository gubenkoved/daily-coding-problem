<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Describe and give an example of each of the following
// types of polymorphism:
// 
// Ad-hoc polymorphism
// Parametric polymorphism
// Subtype polymorphism

void Main()
{
	// I've referred to "ad-hoc" polymorphism as "parametric polimorphsim"
	// and did not refer to "parametric polimorphism" as a polymorphism before
}

// Subtype polimorphsim
//
// Ability to use different types inherited from a single base
// interchangably w/o knowing internal details.
// 
// void Draw(Shape shape)
// ..
// Draw(rectangle)
// Draw(circle)

// Parametric polimorphsim
//
// Ability to write a code in a generic fashion w/o knowing the details
// of the specific type which is going to be used.
//
// class Node<T>
// {
//     public Node<T> Next;
// }


// Ad-hoc polimorphsim
// Refers to functions that can be applied to arguments of different types.
//
// Add(string a, string b)
// Add(int a, int b)
