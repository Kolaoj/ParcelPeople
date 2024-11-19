# ParcelPeople

**Getting Started:**

**Assumptions I made:**

- Envelope charges are the same as package prices except they will never have a surcharge based on dimension as envelopes are lighter.
- The direction of travel and time taken are not business rules which need implementing in the API
- There would be a human check to ensure that the reciever matches the receiver on the shipment as exposed via the API
   -  If I assumed wrongly here, then to implement this I would've created an endpoint to update the shipment to collected but validating the name of the reciever.  

**Things I could've improved on with more time:**

- Written more tests
  - The services could have had more tests
  - I could've used an in memory db to run tests on my repository
 
- Implemented more Swagger Examples
- Added a query param to filter out quotes from the Get customer request

Hope this is helpful, I look forward to speaking to you.
