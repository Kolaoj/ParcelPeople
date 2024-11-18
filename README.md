# ParcelPeople

**Getting Started:**

You'll need to change the path of the SqliteDb to whatever is local for you which matches the sqllite db that is in the repos. It is in the DbContext.cs file on line 51 to the directory. I intended to do it via the API DI and make tha path relative so you wouldn't need to do this but I didn't have the time to.

Apart from that it should just be a case of running it.

I haven't included any swagger examples, it was one of the things I would've done with more time (see below) but here is an example of the JSON you could use when a quote is created:

{
  "senderId": "C2E9E363-2E20-433A-88D6-3C1090D85D52",
  "receiverName": "Bob Bobbington",
  "cities": [
    {
      "origin": true,
      "destination": false,
      "cityId": 2
    },
    {
      "origin": false,
      "destination": true,
      "cityId": 1
    }
  ],
  "parcels": [
    {
      "type": "Envelope",
      "dimensions": 10
    },
	  {
      "type": "Package",
      "dimensions": 50
    }
  ]
}

**Assumptions I made:**

- Envelope charges are not affected by dimenssions
- The direction of travel and time taken do not at this stage require functional changes
- There would be a human check to ensure that the reciever matches the receiver on the shipment
   -  If I assumed wrongly here, then to automate this I would've created an endpoint to update the shipment to collected but validating the name of the reciever.  

**Things I could've improved on with more time:**

- Written more tests
  - The services could have had more tests
  - I could've used an in memory db to run tests on my repository
 
- Implemented Swagger Examples

- Had custom exceptions to handle error scenarios better, e.g. Conflicts (return 419) 

- Done a bit more validation, for example 2 Cities a shipment will pass throgh could both be destinations or origins which would cause a bug. I would not normally rely on the front end to get this right.
- One minor thing, I have annoyingly used a singular for my services/repositories rather than plural e.g.. ICustomerService instead of ICustomersService. I think the plural makes more sense but it was a bit late to change it.


Hope this is helpful, I look forward to speaking to you.
