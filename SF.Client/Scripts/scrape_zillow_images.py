# Scripts/scrape_zillow_images.py

import os
import requests
from bs4 import BeautifulSoup
from urllib.parse import urljoin

# URL of the Zillow page
url = "https://www.zillow.com/homedetails/1405-Vegas-Verdes-Dr-UNIT-304-Santa-Fe-NM-87507/87883989_zpid/"

# Create a directory to save the images
os.makedirs('zillow_images', exist_ok=True)

# Send a GET request to the URL
response = requests.get(url)
response.raise_for_status()  # Check if the request was successful

# Parse the HTML content
soup = BeautifulSoup(response.text, 'html.parser')

# Find all image tags
img_tags = soup.find_all('img')

# Filter out the jpg images and download them
for img in img_tags:
    img_url = img.get('src')
    if img_url and img_url.endswith('.jpg'):
        # Create the full URL
        img_url = urljoin(url, img_url)
        # Get the image content
        img_response = requests.get(img_url)
        img_response.raise_for_status()
        # Get the image name
        img_name = os.path.join('zillow_images', os.path.basename(img_url))
        # Save the image
        with open(img_name, 'wb') as f:
            f.write(img_response.content)
        print(f'Downloaded {img_name}')

print('All images downloaded.')