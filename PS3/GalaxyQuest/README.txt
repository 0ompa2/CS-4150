NASA recently confirmed the discovery of parallel universes (PUs) occupying alternate dimensions. 
These universes are quite different from our own universe, in the following ways:

- Each PU is a two-dimensional square that stretches 10^9 light years 
  from left to right and from top to bottom.

- Each PU has a galactic diameter of d light years, where d is an integer.

- Each star is exactly x light years from its universe�s left edge and y light years from 
  its universe�s bottom edge, where x and y are non-negative integers.

- Stars are clustered into galaxies. Each galaxy consists of one or more stars. 
  Each star is at most d light years from every other star in its galaxy. 
  Any two stars from different galaxies are more than d light years apart.

It is crucial to understand the implications of this. The locations of the stars determine the galaxies. 
The galaxy to which a star s belongs consists of s plus all other stars that are within d light years of s.
Suppose you draw a circle of diameter d around all of the stars of a galaxy. Any star not belonging to 
the galaxy will be outside the circle and will be more than d light years from every star in the galaxy.
For each PU, NASA has obtained all of its stellar coordinates and has measured its value of d.

Given the description of a PU, NASA would like to be able to determine whether that PU has a galaxy 
that contains more than half of the stars in the PU. NASA has turned to you.


- Input -
The input describes a single PU. All numbers in the input are integers.

The first line of the input contains the PU�s galactic diameter d (1 <= d <= 10^6) 
and star count k (1 <= k <= 10^6).

There are exactly k more lines. Each line contains the xx (0 <= x <= 10^9) and 
y (0 <= y <= 10^9) coordinates of a star in the PU. No two of these lines are identical, 
as a black hole would result!

The star positions and d are guaranteed to obey the clustering constraint discussed above.


- Output -
If the PU described by the input has a galaxy containing more than half of the stars, 
display the number of stars in that galaxy. Otherwise, display NO.