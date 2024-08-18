import { Grid, GridItem } from "@chakra-ui/react";
import { GameViewer } from "./GameViewer";
import { GameEditor } from "./GameEditor";


export const GameSession = () => {
    return (
        <Grid flexWrap="wrap"
            templateColumns="repeat(5, 1fr)"
            gap="8"
            p="8"
            minH="100vh">

            <GridItem colSpan={3}>
                <GameViewer/>
            </GridItem>

            <GridItem colSpan={2}>
                <GameEditor/>
            </GridItem>
        </Grid>
    );
}