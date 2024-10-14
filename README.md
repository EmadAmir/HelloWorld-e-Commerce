<h1>Ecommerce Store Using Angular, .NET and Stripe for payment processing.</h1> 

<h3>Project Structure</h3>
API: <i>Manages Http request and responses.</i> </br>
Infrastructure: <i>Communicate with the DB. </i> </br>
Core: <i>Contains the business entities.</i> </br>

<h3>Repository Pattern</h3>
<ul>
  <li>Decouple business code from the data access</li>
  <li>Repository pattern is often used when an application performs data access operations.</li>
  <li>It doesn't matter where the data operations take place, which means it can be on the Database, JSON, Webservice, FileStorage etc.</li>
  <li>The Idea of repository is to encapsulate the data operations.</li>
  <li>Seperation of Concerns: It keeps our controllers clean by avoiding the direct use of DBContext.</li>
  <li>It also minimises duplicate code, because if we have a DBContext injected into several controllers then we would end up repeating query logic accross them.</li>
  <li>Testability: Repository pattern makes it easy to mock a repository rather than DbContext which makes unit testing easier. DbContext is huge and complicated to Mock</li>
</ul>

<h3>Repository Pattern: why are we using in our project? </h3>

<ul>
  <li>We want to avoid messy controllers,  and directly injecting the store context into our controllers.</li>
  <li>Not part of the project but if were to implement testing then it would simplify testing.</li>
  <li>Increases Abstraction: it provides a layer between our controllers and DbContext.</li>
  <li>Maintainability: Its easier to maintain repositories than controllers with direct DbContext access.</li>
  <li>Reduces duplicate code.</li>
</ul>

<h3>Repository Pattern: How are we using in our project </h3>

<ul>
  <li>We inject our Repository interface into our controller. Which gives us access to methods like,
    <ul>
      <li>GetProducts()</li>
      <li>GetProduct(int id)</li>
    </ul>
</li>
  <li>These are the methods that we call from our controller.</li>
<li>Inside our repository we inject DbContext and we can use something eg: "_context.Products.ToList()".</li>
<li>-Then DbContext will translate this code "_context.Products.ToList()" into something our database can understand 
eg: "Select * from Products". All of this via the Entity Framework Provider.</li>
<li>The list of products thats gets fetched from the database is passed on to the repository which inturn gets passed on to the controller.</li>
<li>By this way the API controller and return the data to the client that is requesting the data.</li>
</ul>
