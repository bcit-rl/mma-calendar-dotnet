import * as React from "react";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import Box from "@mui/material/Box";
import FightTabPanel from "./FightTabPanel";
import { a11yProps, createFightList } from "../utils/helpers";
import { useState, useEffect } from "react";

interface FightCarouselProps {
  URL: string;
}

const FightCarousel = (props: FightCarouselProps) => {
  const [value, setValue] = React.useState(0);
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
  const [fightsList, setFights] = useState<React.ReactNode[]>([]);
  const tabLists: React.ReactNode[] = [];
  const tabPanel: React.ReactNode[] = [];

  const cardSegments: readonly [string, string, string] = [
    "Main Card",
    "Prelims",
    "Early Prelims",
  ];

  useEffect(() => {
    const FillFightData = async () => {
      const fight_list = await createFightList(props.URL);
      const tempFightList: React.ReactNode[] = [];

      if (fight_list) {
        for (let i = 0; i < fight_list.length; i++) {
          tempFightList.push(fight_list[i]);
        }
        setFights(tempFightList);
      }
    };
    FillFightData();
  }, []);

  for (let i = 0; i < fightsList.length; i++) {
    tabLists.push(<Tab label={cardSegments[i]} {...a11yProps(i)} />);
    tabPanel.push(
      <FightTabPanel value={value} index={i}>
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
