import { Box } from "@mui/material";

interface Props {
  image?: React.ReactNode;
  imageSide: string;
  fighterName: string;
  fighterRecord: string;
}

const FighterInfo = ({
  image,
  imageSide,
  fighterName,
  fighterRecord
}: Props) => {

  return (
    <Box display="flex" alignContent="center" alignItems="center">
      {imageSide === "L" && image}
      <Box textAlign={imageSide === "L" ? "start" : "end"}>
        <p style={{ marginBottom: "5px" }}>{fighterName}</p>
        <p style={{ color: "Grey", fontSize: "0.8em", marginTop: "5px" }}>
          {fighterRecord}
        </p>
      </Box>
      {imageSide === "R" && image}
    </Box>
  );
};

export default FighterInfo;
