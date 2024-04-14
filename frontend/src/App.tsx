import "./App.css";
import FightCarousel from "./components/FightCarousel";
import FightWidget from "./components/FightWidget";
import { IEventData, binarySearch, createCarousel, createFightCarousel } from "./utils/helpers";
import { ReactNode, useEffect, useState } from "react";

function App() {
  const [eventList, setEventList] = useState<IEventData[]>([]);
  const [carouselList, setCarouselList] = useState<ReactNode[]>([]);
  const URL = "http://localhost:5217/api/events";

  useEffect(() => {
    const getEventData = async () => {
      const response = await fetch(URL);
      const fights = await response.json();
      const tempCarousel = [];
      const tempEventList = [];
      
      for (let i = 0; i < fights.length; i++) {
        const carousel =  <FightCarousel URL={URL + `/${fights[i].eventId}`}></FightCarousel>
        tempCarousel.push(carousel);
        tempEventList.push(fights[i]);
      }
      setEventList(tempEventList);
      setCarouselList(tempCarousel);
    };

    getEventData();
  }, []);

  return (
    <div className="carousel-container">
      <FightWidget
        carouselArray={carouselList}
        eventData={eventList}
      ></FightWidget>
    </div> 
  );
}

export default App;
