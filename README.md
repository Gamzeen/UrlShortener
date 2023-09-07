# UrlShortener

This simple .NET API can be used to shorten long URLs, making them easier to share. With this API, you can create your own custom URL shortening service.

## Getting Started
These instructions include the basic steps required to run or deploy the project on your local machine.

## Installation

Clone this repository to your local machine:

```bash 
  git clone https://github.com/Gamzeen/UrlShortener.git
```

## Usage

#### The POST method is used to save a long URL as a short URL in your database.

```http
  POST /api/shorten
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `url` | `string` | The **URL you want to shorten** |

If you enter the short URL into the address bar, you will be immediately redirected to the long URL.

  
