import { Table, TableContainer, Tbody, Text, Th, Thead, Tr } from "@chakra-ui/react";

export const PlayerList = () =>
{
    return (
        <div className="p-2">

            <Text as="b">Players</Text>

            <TableContainer>
                <Table variant="striped">
                    <Thead>
                        <Tr>
                            <Th>Place</Th>
                            <Th>Name</Th>
                            <Th isNumeric>Score</Th>
                        </Tr>
                    </Thead>
                    
                    <Tbody>
                        
                    </Tbody>
                </Table>
            </TableContainer>
        </div>
    );
}