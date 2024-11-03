import { AlertDialog, AlertDialogBody, AlertDialogContent, AlertDialogFooter, AlertDialogHeader, AlertDialogOverlay, Button, FormControl, FormLabel, Input, SelectField, Text } from "@chakra-ui/react";
import { FocusableElement } from "@chakra-ui/utils";
import { SyntheticEvent, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";

const url = "http://localhost:5074/api/session";

interface Props {
    onClose: () => void
    isOpen: boolean
}

export const RoomCreationForm = ({ onClose, isOpen }: Props) => {

    const navigate = useNavigate();
    const cancelRef = useRef<HTMLButtonElement | FocusableElement>(null);
    const [loading, setLoading] = useState(false);
    const [name, setName] = useState("");
    const [pass, setPass] = useState("");
    const [playerCount, setPlayerCount] = useState(2);

    const onSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();

        setLoading(true);

        const data =
        {
            name: name,
            password: pass,
            playerCount: playerCount
        };

        const options: RequestInit =
        {
            method: "POST",
            credentials: "include",
            headers:
            {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        };

        try {
            var result = await fetch(url, options);
        }
        catch {
            alert("server internal error");
            setLoading(false);
            return;
        }

        if (result.ok) {
            const id = await result.json();
            navigate(`/session/${id}`);
        }
        else if (result.status === 401)
        {
            navigate("/signin");
        }
        else {
            setLoading(false);
        }
    }

    return (
        <AlertDialog
            motionPreset="slideInBottom"
            onClose={onClose}
            isOpen={isOpen}
            leastDestructiveRef={cancelRef}
            isCentered>

            <AlertDialogOverlay />

            <AlertDialogContent bg="white">
                <AlertDialogHeader fontSize={18} color="green">
                    <Text>
                        New room
                    </Text>
                </AlertDialogHeader>

                <form onSubmit={onSubmit}>
                    <AlertDialogBody>

                        <FormControl mb="4">
                            <FormLabel>Name</FormLabel>
                            <Input placeholder="name" required onChange={x => setName(x.target.value)}/>
                        </FormControl>

                        <FormControl mb="4">
                            <FormLabel>Password (if needed)</FormLabel>
                            <Input placeholder="password" type="password" onChange={x => setPass(x.target.value)}/>
                        </FormControl>

                        <FormControl mb="4">
                            <FormLabel>number of players</FormLabel>
                            <SelectField onChange={x => setPlayerCount(+x.target.value)}>
                                <option value={2}>2</option>
                                <option value={3}>3</option>
                                <option value={4}>4</option>
                                <option value={5}>5</option>
                                <option value={6}>6</option>
                                <option value={7}>7</option>
                                <option value={8}>8</option>
                            </SelectField>
                        </FormControl>
                    </AlertDialogBody>

                    <AlertDialogFooter>
                        <Button colorScheme="green" type="submit"
                            loadingText="creating" isLoading={loading}>
                            Create
                        </Button>

                        {
                            loading === false ?
                                <Button colorScheme="red" ml={3} onClick={onClose}>Cancel</Button>
                                : <></>
                        }
                    </AlertDialogFooter>
                </form>

            </AlertDialogContent>
        </AlertDialog >

    );
}