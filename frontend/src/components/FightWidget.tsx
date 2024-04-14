import { Box, Tab, Tabs } from "@mui/material";
import React, { useEffect } from "react";
import { IEventData, a11yProps, binarySearch } from "../utils/helpers";
import FightWidgetTabPanel from "./FightWidgetTabPanel";

interface FightWidgetProps {
  carouselArray?: Array<React.ReactNode>;
  eventData: Array<IEventData>;
}

const FightWidget = (props: FightWidgetProps) => {
  const [value, setValue] = React.useState(0);
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
  const tabList: Array<React.ReactNode> = [];
  const tabPanels: Array<React.ReactNode> = [];
  
  // useEffect(() => {
  //   setValue(binarySearch(props.eventData, new Date))
  // }, [props.eventData]);

  if (props.carouselArray && props.eventData) {
    for (let i = 0; i < props.carouselArray.length; i++) {
      tabList.push(<Tab label={props.eventData[i].eventName} {...a11yProps(i)} />);
      tabPanels.push(
        <FightWidgetTabPanel value={value} index={i}>
          {props.carouselArray[i]}
        </FightWidgetTabPanel>
      );
    }
  }

  return (
    <Box sx={{ minWidth:500, maxWidth: 800 }}>
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs
          value={value}
          onChange={handleChange}
          variant="scrollable"
          aria-label="basic tabs example"
        >
          {tabList}
        </Tabs>
      </Box>
      {tabPanels}
    </Box>
  );
};

export default FightWidget;
