import "./App.css";
import FightWidget from "./components/FightWidget";
import { createCarousel, createFightCarousel } from "./utils/helpers";
import { ReactNode, useEffect, useState } from "react";

function App() {
  const [eventList, setEventList] = useState<string[]>([]);
  const [carouselList, setCarouselList] = useState<ReactNode[]>([]);

  const URL = "http://localhost:5217/api/events";
  useEffect(() => {
    const getEventData = async () => {
      const response = await fetch(URL);
      const fights = await response.json();
      const tempCarousel = [];
      const tempEventList = [];
      for (let i = 0; i < fights.length; i++) {
        const currentResult = await fetch(URL + `/${fights[i].eventId}`);
        const currentEvent = await currentResult.json();
        const carousel = await createFightCarousel(currentEvent)
        tempCarousel.push(carousel);
        tempEventList.push(fights[i].eventName);
      }

      setEventList(tempEventList);
      setCarouselList(tempCarousel);
      
    };

    getEventData();
  }, []);

  return (
    // <div className="carousel-container">
      <FightWidget
        carouselArray={carouselList}
        eventNames={eventList}
      ></FightWidget>
    // {/* </div> */}
  );
}

export default App;
