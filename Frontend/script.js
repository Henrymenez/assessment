const SearchButton = document.querySelector(".search-button");
const Search = document.querySelector("#search");
const title = document.querySelector(".MovieTitle");
const year = document.querySelector(".releaseYear");
const rated = document.querySelector(".Rated");
const runtime = document.querySelector(".Runtime");
const genre = document.querySelector(".genre");
const plot = document.querySelector(".Plot");
const language = document.querySelector(".Language");
const imdbRating = document.querySelector(".imdbRating");
const type = document.querySelector(".Type");
const country = document.querySelector(".Country");
const website = document.querySelector(".website");
const image = document.querySelector(".image");
const moviePoster = document.getElementById('moviePoster');
const director =  document.querySelector(".director");
const actors = document.querySelector(".Actors");
      
const baseUrl =  "https://localhost:7102"; 

document.addEventListener("DOMContentLoaded", function () {
  let titlequery;
  document.querySelector('form').addEventListener('submit', function (event) {
    event.preventDefault();
  });
  SearchButton.addEventListener("click", function () {
    titlequery = Search.value.trim();
    console.log(Search.value);
    // Convert the data to a JSON string
    const fullUrl = `${baseUrl}/api/Movie/search?MovieTitle=${encodeURIComponent(titlequery)}`;  
   

  fetch(fullUrl, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => {
      if (!response.ok) {
     
     throw new  Error("Something went wrong,please try again later");
      }
      
      return response.json();
    })
    .then((data) => {
      // Handle the response from the API, e.g., show a success message
      console.log("Event added successfully:", data);
     ChangeCardInfo(data.data);
    })
    .catch((error) => {
      alert(error);
      console.error(error.message);
    });
    function ChangeCardInfo(data) {
      title.textContent = data.title == 'N/A'? 'Movie Title' : data.title;
      actors.textContent = data.actors;
      year.textContent = data.year;
      rated.textContent = data.rated;
      runtime.textContent = data.runtime;
       genre.textContent = data.genre;
       director.textContent = data.director;
      plot.textContent = data.plot;
      language.textContent = data.language;
      country.textContent = data.country;
      imdbRating.textContent = data.imdbRating;
      image.src = data.poster == 'N/A' ? 'https://via.placeholder.com/300': data.poster;
      type.textContent = data.type;
    }
  });


  


  fetch(`${baseUrl}/api/Movie/last-five-entries`)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      // Call a function to create cards using the retrieved data
      searchs(data.data);
    })
    .catch((error) => {
      console.error("Error fetching event data:", error);
    });

  function searchs(data) {
    const lastSearchesContainer = document.querySelector('.last-searches');
    lastSearchesContainer.innerHTML = data.map(data => `<p class="last-search-item">${data}</p>`).join('-');
  }
});
