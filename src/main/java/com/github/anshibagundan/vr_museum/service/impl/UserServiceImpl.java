package com.github.anshibagundan.vr_museum.service.impl;

import com.github.anshibagundan.vr_museum.dto.UserDto;
import com.github.anshibagundan.vr_museum.entity.User;
import com.github.anshibagundan.vr_museum.exception.ResourceNotFoundException;
import com.github.anshibagundan.vr_museum.mapper.UserMapper;
import com.github.anshibagundan.vr_museum.repository.UserRepository;
import com.github.anshibagundan.vr_museum.service.UserService;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
@AllArgsConstructor
public class UserServiceImpl implements UserService {

    private UserRepository userRepository;

    @Override
    public UserDto createUser(UserDto userDto) {

        User user = UserMapper.mapToUser(userDto);
        User savedUser = userRepository.save(user);
        return UserMapper.mapToUserDto(savedUser);
    }

    @Override
    public UserDto getUserById(Long userId) {
        User user = userRepository.findById(userId)
                .orElseThrow(() ->
                        new ResourceNotFoundException("User is not exists with given id : " + userId));
        return UserMapper.mapToUserDto(user);
    }

    @Override
    public List<UserDto> getAllUser() {
        List<User> task = userRepository.findAll();
        return task.stream().map((user) -> UserMapper.mapToUserDto(user))
                .collect(Collectors.toList());
    }

    @Override
    public UserDto updateUser(Long userId, UserDto updatedUser) {

        User user = userRepository.findById(userId).orElseThrow(
                () -> new ResourceNotFoundException("User is not exists with given id : " + userId)
        );

        user.setUsername(updatedUser.getUsername());
        user.setPassword(updatedUser.getPassword());

        User updatedUserObj = userRepository.save(user);

        return UserMapper.mapToUserDto(updatedUserObj);
    }

    @Override
    public void deleteUser(Long userId) {

        User user = userRepository.findById(userId).orElseThrow(
                () -> new ResourceNotFoundException("User is not exists with given id : " + userId)
        );

        userRepository.deleteById(userId);

    }
}
