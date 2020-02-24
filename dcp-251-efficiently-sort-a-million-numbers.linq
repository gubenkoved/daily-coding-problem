<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given an array of a million integers between zero and a billion,
// out of order, how can you efficiently sort it? Assume that you
// cannot store an array of a billion elements in memory.

void Main()
{
	// so... counting sort does not work as we'd need to store
	// amount of numbers equal to range, which is forbidden
	// by the problem statement explicitly
	//
	// may be we can make use of another restriction? like the
	// limited amount of the numbers themselves?
	//
	// is there something better than simple Quick Sort or other
	// general purpose algorithms?
	
	
	// if a million can fit into the memory, then regular general
	// purpose Quick Sort will do, if not, then we would need to use
	// external memory, like  splitting data in chunks for subsequent
	// merge sort using external memory
	
}