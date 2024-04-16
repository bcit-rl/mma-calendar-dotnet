import { Box, Typography } from "@mui/material";
import {ReactNode} from "react";

interface Props {
  image?: ReactNode;
  imageSide: string;
  fighterName: string;
  fighterRecord: string;
}

const FighterInfo = ({
  image,
  imageSide,
  fighterName,
  fighterRecord,
}: Props) => {
  return (
    <Box
      display="flex"
      alignContent="center"
      alignItems="center"
      sx={{ marginBottom: "10px", marginTop: "10px" }}
    >
      {imageSide === "L" && image}
      <Box textAlign={imageSide === "L" ? "start" : "end"}>
        <Typography variant="body1" sx={{ marginBottom: "5px" }}>
          {fighterName}
        </Typography>
        <Typography
          variant="body1"
          sx={{ color: "Grey", fontSize: "0.8em", marginTop: "5px" }}
        >
          {fighterRecord}
        </Typography>
      </Box>
      {imageSide === "R" && image}
    </Box>
  );
};

export default FighterInfo;
