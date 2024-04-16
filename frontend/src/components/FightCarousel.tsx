import * as React from "react";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import Box from "@mui/material/Box";
import FightTabPanel from "./FightTabPanel";
import { a11yProps, createFightList } from "../utils/helpers";
import { useState, useEffect,ReactNode, SyntheticEvent } from "react";

interface FightCarouselProps {
  URL: string;
}

const cache : {[key:string] : ReactNode[]} = {}

const FightCarousel = (props: FightCarouselProps) => {
  const [value, setValue] = useState(0);
  const handleChange = (event: SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
  const [fightsList, setFights] = useState<ReactNode[]>([]);
  const tabLists: ReactNode[] = [];
  const tabPanel: ReactNode[] = [];

  const cardSegments: readonly [string, string, string] = [
    "Main Card",
    "Prelims",
    "Early Prelims",
  ];

  useEffect(() => {
    const FillFightData = async () => {
      if(cache[props.URL]){
        setFights(cache[props.URL])
        return
      }

      const fight_list = await createFightList(props.URL);
      const tempFightList: ReactNode[] = [];
      if (fight_list) {
        for (let i = 0; i < fight_list.length; i++) {
          tempFightList.push(fight_list[i]);
        }
        setFights(tempFightList);
      }
      cache[props.URL] = tempFightList
    };
    FillFightData();
  }, []);

  for (let i = 0; i < fightsList.length; i++) {
    tabLists.push(<Tab key={`CarouselTab-${i}`} label={cardSegments[i]} {...a11yProps(i)} />);
    tabPanel.push(
      <FightTabPanel key={`CarouselTabPanel-${i}`}value={value} index={i}>
        {fightsList[i]}
      </FightTabPanel>
    );
  }

  return (
    <Box>
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs
          value={value}
          onChange={handleChange}
          variant="scrollable"
          aria-label="basic tabs example"
        >
          {tabLists}
        </Tabs>
      </Box>
      {tabPanel}
    </Box>
  );
};

export default FightCarousel;
