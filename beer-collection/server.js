const express = require('express');
const app = express();
const port = 3000;

app.use(express.json());

// In-memory beer collection
const beerCollection = [];

// Endpoint for adding a new beer
app.post('/api/beers', (req, res) => {
    const beer = req.body;
    beerCollection.push(beer);
    res.sendStatus(201);
});

// Endpoint for listing all beers
app.get('/api/beers', (req, res) => {
    res.json(beerCollection);
});

// Endpoint for searching a beer by name
app.get('/api/beers/:searchTerm', (req, res) => {
    const searchTerm = req.params.searchTerm.toLowerCase();
    const matchingBeers = beerCollection.filter(beer => beer.name.toLowerCase().includes(searchTerm));
    res.json(matchingBeers);
});

// Endpoint for updating a beer's rating/add new ratinng
app.patch('/api/beers/:name/rate', (req, res) => {
    const name = req.params.name;
    const rating = req.body.rating;
    const beerToUpdate = beerCollection.find(beer => beer.name === name);

    if (beerToUpdate) {
        if (beerToUpdate.rating) {
            // Calculate the average rating
            beerToUpdate.rating = (beerToUpdate.rating + rating) / 2;
        } else {
            beerToUpdate.rating = rating;
        }
        res.sendStatus(200);
    } else {
        res.sendStatus(404);
    }
});

app.listen(port, () => {
    console.log(`Server is running on port ${port}`);
});
