# Free food
*A functional food bank site built in an evening for Battle Hack London 2013.*

This is a functional sample of a "food bank" site. The ideas of food banks is matching people who want to donate food with people who need it.

## Why?

At least four million people in the UK do not have access to a healthy diet; nearly 13 million people live below the poverty line, and it is becoming harder and harder for them to afford healthy food. Lower-income families in the UK have cut their consumption of fruit and vegetables by nearly a third in the wake of the recession and rising food prices.

**Let us make sure no one goes to bed hungry anymore.**

## What's in the package?

A complete ASP.NET MVC4 site and database backup. The site has the following features:

Feeding

* Geographical search of available food for people in need.
* Coupon generation for booking meals

Cash donations

* Donations via PayPal, single meal, weekly shop, Christmas dinner or custom amount

Supermarket donations

* AJAXy page to pledge food.

Administration (`/admin`) 
 
* Impersonation
* Tallies

## What's missing

* Authentication
* Support for restaurant donations
* CRUDs for donors
* More advanced admin

# Installation

Clone the project and build in Visual Studio. Restore the DB back up located in `./SqlServer/FreeFood.bak` via SSMS. Run the migration scripts in `./SqlServer/` in numerical order.

# License

Copyright &copy; 2013&ndash;2015, Marco Cecconi and Dave Hillier.  
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.