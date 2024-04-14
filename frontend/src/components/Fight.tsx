import { Box, Container, Typography } from "@mui/material";
import { ReactNode } from "react";

interface Props {
  leftFighter: ReactNode;
  rightFighter: ReactNode;
  date: string;
  weightClass: string;
  description?: string;
}

const Fight = ({
  leftFighter,
  rightFighter,
  date,
  weightClass,
  description,
}: Props) => {
  const formatted_date = new Date(date)


  return (
    <Container sx={{ border: 1, borderColor: "black"}}>
      <Box>
        <Typography variant="subtitle2">{`${formatted_date.toLocaleString()}`}</Typography>
        <Typography variant="subtitle1">{weightClass}</Typography>
      </Box>
      <Box display={"flex"} justifyContent={"space-between"}>
        {leftFighter}
        {rightFighter}
      </Box>
      {description && (
        <Typography variant="subtitle1" color={"green"}>
          {description}
        </Typography>
      )}
    </Container>
  );
};

export default Fight;
