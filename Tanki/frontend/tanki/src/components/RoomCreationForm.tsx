import { AlertDialog, AlertDialogBody, AlertDialogContent, AlertDialogFooter, AlertDialogHeader, AlertDialogOverlay, Button, Input, Text } from "@chakra-ui/react";
import { FocusableElement } from "@chakra-ui/utils";
import { RefObject, useRef } from "react";

interface Props {
    onClose: () => void
    isOpen: boolean
}

export const RoomCreationForm = ({ onClose, isOpen }: Props) => {

    const cancelRef = useRef<HTMLButtonElement | FocusableElement>(null);

    return (
        <AlertDialog
            motionPreset="slideInBottom"
            onClose={onClose}
            isOpen={isOpen}
            leastDestructiveRef={cancelRef}
            isCentered
        >

            <AlertDialogOverlay />

            <AlertDialogContent bg="white">
                <AlertDialogHeader fontSize={18} color="green">
                    <Text>
                        New room
                    </Text>
                </AlertDialogHeader>

                <AlertDialogBody>
                    <div className="mb-4">
                        <Text className="mb-3">
                            input name of new room
                        </Text>

                        <Input placeholder="name" />
                    </div>
                </AlertDialogBody>


                <AlertDialogFooter>
                    <Button colorScheme="green">Create</Button>
                    <Button colorScheme="red" ml={3} onClick={onClose}>Cancel</Button>
                </AlertDialogFooter>

            </AlertDialogContent>
        </AlertDialog>

    );
}