import { Center, Spinner, Table, TableContainer, Tbody, Th, Thead, Tr } from "@chakra-ui/react";

export const PlayerList = () =>
{
    return (
        <div className="bg-gray-800 rounded">
            <TableContainer>
                <Table variant="striped" colorScheme="steal">
                    <Thead>
                        <Tr>
                            <Th>Place</Th>
                            <Th>Name</Th>
                            <Th>Score</Th>
                        </Tr>
                    </Thead>
                    
                    <Tbody>
                    </Tbody>
                </Table>
            </TableContainer>
        </div>
    );
}